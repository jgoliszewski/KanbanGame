using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class CardService : ICardService
{
    private int lastId;
    private readonly IKanbanTaskService _kanbanTaskService;
    private readonly IEmployeeService _employeeService;
    private readonly HttpClient _http;
    public CardService(HttpClient http, IKanbanTaskService kanbanTaskService, IEmployeeService employeeService)
    {
        _http = http;
        _kanbanTaskService = kanbanTaskService;
        _employeeService = employeeService;
    }
    public List<Card> Cards { get; set; }
    public Dictionary<string, int> ColumnCount;
    public async Task GetCards()
    {
        lastId = 0;
        Cards = new List<Card>();
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

        foreach (var t in _kanbanTaskService.KanbanTasks)
        {
            if (t.Employee is not null)
            {
                Cards.Add(EmployeeToCard(t.Employee, ColumnCount[t.Employee.CurrentRoleString]++));
                t.StatusString = t.Employee.CurrentRoleString; //todo: make it has more sense
                Cards.Add(TaskToCard(t, ColumnCount[t.Employee.CurrentRoleString]++));
                _employeeService.Employees.RemoveAll(e => e.Id == t.Employee.Id);
            }
            else
            {
                Cards.Add(TaskToCard(t, ColumnCount[t.StatusString]++));
            }
        }
        foreach (var e in _employeeService.Employees)
        {
            Cards.Add(EmployeeToCard(e, ColumnCount[e.CurrentRoleString]++));
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
            await _http.PutAsJsonAsync($"api/kanbanTask/{card.KanbanTask.Id}", card.KanbanTask);
        }
        else if (card.KanbanTask is null && card.Employee is not null)
        {
            await _http.PutAsJsonAsync($"api/employee/{card.Employee.Id}", card.Employee);
        }
    }

    public async Task UpdateCards()
    {
        foreach (var card in Cards)
        {
            if (card.KanbanTask is not null)
            {
                //todo: better card above function; card ranks could have difference > 1T
                var cardAbove = Cards.Where(c => c.Column == card.Column && c.RankId == card.RankId - 1).FirstOrDefault();

                if (cardAbove is not null && cardAbove.Employee is not null)
                {
                    if (card.KanbanTask.Employee is null || card.KanbanTask.Employee.Id != cardAbove.Employee.Id)
                    {
                        // Console.WriteLine($"Above {card.KanbanTask.Title} is {cardAbove.Employee.Name}");
                        card.KanbanTask.Employee = cardAbove.Employee;
                        card.KanbanTask.EmployeeId = cardAbove.Employee.Id;
                        await UpdateCard(card.Id, card);
                    }
                }
                else
                {
                    if (card.KanbanTask.Employee is not null)
                    {
                        card.KanbanTask.Employee = null;
                        card.KanbanTask.EmployeeId = null;
                        await UpdateCard(card.Id, card);
                    }
                }
            }
        }
    }
}