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
            {"Analysis", 9},
            {"DevelopmentDoing", 9},
            {"TestDoing", 9},
        };
    }
    public List<Card> Cards { get; set; }
    public Dictionary<string, int> ColumnMaxCount { get; set; }
    private Dictionary<string, int> ColumnCount;
    private Dictionary<Employee, KanbanTask?> Board;
    private List<KanbanTask> tempTasks;

    public async Task GetCards()
    {
        lastId = 0;
        Cards = new List<Card>();
        ColumnMaxCount = new Dictionary<string, int>{
            {"Backlog", 6},
            {"Analysis", 0},
            {"DevelopmentDoing", 0},
            {"TestDoing", 0},
        };
        ColumnCount = new Dictionary<string, int>{
            {"Backlog", 1},
            {"Analysis", 1},
            {"DevelopmentWaiting", 1},
            {"DevelopmentDoing", 1},
            {"TestWaiting", 1},
            {"TestDoing", 1},
            {"Delivered", 1},
        };

        await _kanbanTaskService.GetKanbanTasks();
        await _employeeService.GetEmployees();

        Board = new Dictionary<Employee, KanbanTask?>();
        tempTasks = new List<KanbanTask>();
        foreach (var e in _employeeService.Employees)
        {
            Board.Add(e, null);
            ColumnMaxCount[e.CurrentRoleString] += 2;
        }
        foreach (var t in _kanbanTaskService.KanbanTasks)
        {
            if (t.Employee is not null)
            {
                t.StatusString = t.Employee.CurrentRoleString; //todo: make it has more sense
                var key = Board.Where(p => p.Key.Id == t.Employee.Id).FirstOrDefault().Key;
                Board[key] = t;
            }
            else
            {
                tempTasks.Add(t);
            }
        }
        foreach (var (e, t) in Board)
        {
            Cards.Add(EmployeeToCard(e, ColumnCount[e.CurrentRoleString]));
            ColumnCount[e.CurrentRoleString] += 2;
            if (t is not null)
            {
                Cards.Add(TaskToCard(t, ColumnCount[e.CurrentRoleString]));
                ColumnCount[e.CurrentRoleString] += 2;
            }
        }
        foreach (var t in tempTasks)
        {
            Cards.Add(TaskToCard(t, ColumnCount[t.StatusString]));
            ColumnCount[t.StatusString] += 2;
        }
    }

    private Card TaskToCard(KanbanTask kanbanTask, int rankId)
    {
        var card = new Card()
        {
            Id = lastId++,
            RankId = rankId,
            KanbanTask = kanbanTask,
            Column = kanbanTask.StatusString
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
            Column = employee.CurrentRoleString
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
                    if (card.KanbanTask.Employee is null || card.KanbanTask.Employee.Id != cardAbove.Employee.Id)
                    {
                        card.KanbanTask.Employee = cardAbove.Employee;
                        card.KanbanTask.StatusString = cardAbove.Employee.CurrentRoleString;
                    }
                }
                else
                {
                    if (card.KanbanTask.Employee is not null)
                    {
                        card.KanbanTask.Employee = null;
                    }
                }
                // card.KanbanTask.UpdateTaskStatus(); 
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
}