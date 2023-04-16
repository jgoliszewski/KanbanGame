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
            {"Backlog", 0},
            {"Analysis", 0},
            {"DevelopmentWaiting", 0},
            {"DevelopmentDoing", 0},
            {"TestWaiting", 0},
            {"TestDoing", 0},
            {"Delivered", 0},
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
        if (card.Employee is null)
        {
            await _http.PutAsJsonAsync($"api/kanbanTask/{card.KanbanTask.Id}", card.KanbanTask);
        }
        else if (card.KanbanTask is null)
        {
            await _http.PutAsJsonAsync($"api/employee/{card.Employee.Id}", card.Employee);
        }
    }
}