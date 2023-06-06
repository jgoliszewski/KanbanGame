using KanbanGame.Shared;
using KanbanGame.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanGame.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _EmployeeService;

    public EmployeeController(IEmployeeService EmployeeService)
    {
        _EmployeeService = EmployeeService;
    }

    [HttpGet]
    public async Task<List<Employee>> GetEmployees()
    {
        return await _EmployeeService.GetEmployees();
    }

    [HttpGet("active")]
    public async Task<List<Employee>> GetActiveEmployees()
    {
        return await _EmployeeService.GetActiveEmployees();
    }

    [HttpGet("team/{id}")]
    public async Task<List<Employee>?> GetEmployeesByTeamId(int id)
    {
        return await _EmployeeService.GetEmployeesByTeamId(id);
    }

    [HttpGet("{id}")]
    public async Task<Employee?> GetEmployeeById(int id)
    {
        return await _EmployeeService.GetEmployeeById(id);
    }

    [HttpPost]
    public async Task<Employee?> CreateEmployee(Employee Employee)
    {
        return await _EmployeeService.CreateEmployee(Employee);
    }

    [HttpPut("{id}")]
    public async Task<Employee?> UpdateEmployee(int id, Employee Employee)
    {
        return await _EmployeeService.UpdateEmployee(id, Employee);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteEmployee(int id)
    {
        return await _EmployeeService.DeleteEmployee(id);
    }
}
