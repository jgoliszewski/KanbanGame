using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class CardService : ICardService
{
    private readonly IKanbanTaskService _kanbanTaskService;
    private readonly IEmployeeService _employeeService;
    public CardService(IKanbanTaskService kanbanTaskService, IEmployeeService employeeService)
    {
        _kanbanTaskService = kanbanTaskService;
        _employeeService = employeeService;
    }
    public List<Card> Cards { get; set; }

    public async Task GetCards()
    {
        Cards = new List<Card>();
        await _kanbanTaskService.GetKanbanTasks();
        foreach (var t in _kanbanTaskService.KanbanTasks)
        {
            Cards.Add(TaskToCard(t));
        }
        await _employeeService.GetEmployees();
        foreach (var e in _employeeService.Employees)
        {
            Cards.Add(EmployeeToCard(e));
        }
    }

    private static int lastId = 0;
    private Card TaskToCard(KanbanTask kanbanTask)
    {
        var card = new Card()
        {
            Id = lastId++,
            KanbanTask = kanbanTask,
            Column = kanbanTask.StatusString
        };
        return card;
    }

    private Card EmployeeToCard(Employee employee)
    {
        var card = new Card()
        {
            Id = lastId++,
            Employee = employee,
            Column = employee.CurrentRoleString
        };
        return card;
    }
}