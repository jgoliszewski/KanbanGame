using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class BoardService : IBoardService
{
    private int lastId;
    private readonly IKanbanTaskService _kanbanTaskService;
    private readonly IEmployeeService _employeeService;
    private readonly HttpClient _http;
    public BoardService(HttpClient http, IKanbanTaskService kanbanTaskService, IEmployeeService employeeService)
    {
        _http = http;
        _kanbanTaskService = kanbanTaskService;
        _employeeService = employeeService;
        ColumnMaxCount = new Dictionary<string, int>{
            {"Backlog", 9},
            {"AnalysisDoing", 9},
            {"DevelopmentDoing", 9},
            {"TestDoing", 9},
        }; // For SF Initialization
    }

    public List<Card> Cards { get; set; }
    public Dictionary<string, int> ColumnMaxCount { get; set; }
    private Dictionary<string, int> ColumnCount;
    private Dictionary<Employee, KanbanTask?> BoardBuild;
    private List<KanbanTask> TempTasks;

    public async Task GetCards()
    {
        InitProperties();
        
        await _kanbanTaskService.GetKanbanTasks();
        await _employeeService.GetEmployees();

        foreach (var e in _employeeService.Employees)
        {
            BoardBuild.Add(e, null);
            ColumnMaxCount[e.SF_Column] += 2;
        }

        foreach (var t in _kanbanTaskService.KanbanTasks)
        {
            if (t.Employee is not null)
            {
                t.SF_Column = t.Employee.SF_Column;
                var key = BoardBuild.Where(p => p.Key.Id == t.Employee.Id).FirstOrDefault().Key;
                BoardBuild[key] = t;
            }
            else
            {
                TempTasks.Add(t);
            }
        } 

        foreach (var (e, t) in BoardBuild)
        {
            Cards.Add(EmployeeToCard(e, ColumnCount[e.SF_Column]));
            ColumnCount[e.SF_Column] += 2;
            if (t is not null)
            {
                Cards.Add(TaskToCard(t, ColumnCount[e.SF_Column]));
                ColumnCount[e.SF_Column] += 2;
            }
        }
        foreach (var t in TempTasks)
        {
            Cards.Add(TaskToCard(t, ColumnCount[t.SF_Column]));
            ColumnCount[t.SF_Column] += 2;
        }
    }

    private Card TaskToCard(KanbanTask kanbanTask, int rankId)
    {
        var card = new Card()
        {
            Id = lastId++,
            RankId = rankId,
            KanbanTask = kanbanTask,
            Column = kanbanTask.SF_Column
        };
        return card;
    }

    private Card EmployeeToCard(Employee employee, int rankId)
    {
        var card = new Card()
        {
            Id = lastId++,
            RankId = rankId,
            Employee = employee,
            Column = employee.SF_Column
        };
        return card;
    }

    public async Task UpdateCard(int cardId, Card card)
    {
        if (card.Employee is null && card.KanbanTask is not null)
        {
            await _kanbanTaskService.UpdateKanbanTask(card.KanbanTask.Id, card.KanbanTask);
        }
        else if (card.KanbanTask is null && card.Employee is not null)
        {
            await _employeeService.UpdateEmployee(card.Employee.Id, card.Employee);
        }
    }
    public async Task UpdateCardLocal(int cardId, Card card)
    {
        var dbCard = Cards.Where(c => c.Id == cardId).FirstOrDefault();
        if (dbCard is not null)
        {
            dbCard.Column = card.Column;
            dbCard.RankId = card.RankId;
            dbCard.Employee = card.Employee;
            dbCard.KanbanTask = card.KanbanTask;
        }
    }

    public async Task UpdateColumn(string column)
    {
        foreach (var card in Cards.Where(c => c.Column == column))
        {
            if (card.KanbanTask is not null)
            {

                var cardAbove = FindCardAbove(card);

                if (cardAbove is not null && cardAbove.Employee is not null)
                {
                    card.KanbanTask.Employee = cardAbove.Employee;
                    card.KanbanTask.SF_Column = cardAbove.Employee.SF_Column;
                }
                else
                {
                    card.KanbanTask.Employee = null;
                } 
            }
            await UpdateCard(card.Id, card);
        }
    }

    private Card? FindCardAbove(Card card)
    {
        var column = Cards.Where(c => c.Column == card.Column && c.RankId < card.RankId);
        if (column.ToList().Count > 0)
        {
            var cardAbove = column.MaxBy(c => c.RankId);
            return cardAbove;
        }
        return null;
    }

    private void InitProperties()
    {
        lastId = 0;
        Cards = new List<Card>();
        ColumnMaxCount = new Dictionary<string, int>{
            {"Backlog", 6},
            {"AnalysisDoing", 0},
            {"DevelopmentDoing", 0},
            {"TestDoing", 0},
        };
        ColumnCount = new Dictionary<string, int>{
            {"Backlog", 1},
            {"AnalysisDoing", 1},
            {"DevelopmentWaiting", 1},
            {"DevelopmentDoing", 1},
            {"TestWaiting", 1},
            {"TestDoing", 1},
            {"Delivered", 1},
        };
        BoardBuild = new Dictionary<Employee, KanbanTask?>();
        TempTasks = new List<KanbanTask>();
    }
}