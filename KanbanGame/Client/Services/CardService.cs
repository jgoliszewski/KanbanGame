using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class CardService : ICardService
{
    private readonly IKanbanTaskService _kanbanTaskService;
    private readonly IEmployeeService _employeeService;
    public CardService(KanbanTaskService kanbanTaskService, EmployeeService employeeService)
    {
        _kanbanTaskService = kanbanTaskService;
        _employeeService = employeeService;
    }
    public List<Card> Cards { get; set; } = new List<Card>();

    public async Task<List<Card>> GetCards()
    {
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
        return Cards;
    }

    public Task GetCardsByColumnId()
    {
        throw new NotImplementedException();
    }

    Task<Employee?> ICardService.GetCardById(int employeeId)
    {
        throw new NotImplementedException();
    }

    private Card TaskToCard(KanbanTask kanbanTask)
    {
        var card = new Card()
        {
            KanbanTask = kanbanTask,
            StatusString = "Doing"
        };
        return card;
    }

    private Card EmployeeToCard(Employee employee)
    {
        var card = new Card()
        {
            Employee = employee,
            StatusString = "Doing"
        };
        return card;
    }
}