using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigationManger;

    public EmployeeService(HttpClient http, NavigationManager navigationManger)
    {
        _http = http;
        _navigationManger = navigationManger;
    }
    public List<Employee> Employees { get; set; } = new List<Employee>();

    public async Task GetEmployees()
    {
        var result = await _http.GetFromJsonAsync<List<Employee>>("api/employee");
        if (result is not null)
            Employees = result;
    }
    public async Task<Employee?> GetEmployeeById(int employeeId)
    {
        var result = await _http.GetAsync($"api/employee/{employeeId}");
        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await result.Content.ReadFromJsonAsync<Employee>();
        }
        return null;
    }
    public async Task CreateEmployee(Employee employee)
    {
        await _http.PostAsJsonAsync("api/employee", employee);
        _navigationManger.NavigateTo("employees");
    }

    public async Task UpdateEmployee(int employeeId, Employee employee)
    {
        await _http.PutAsJsonAsync($"api/employee/{employeeId}", employee);
        _navigationManger.NavigateTo("employees");
    }
    public async Task DeleteEmployee(int employeeId)
    {
        var result = await _http.DeleteAsync($"api/employee/{employeeId}");
        _navigationManger.NavigateTo("employees");
    }
}