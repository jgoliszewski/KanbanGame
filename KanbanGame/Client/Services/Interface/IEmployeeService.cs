using KanbanGame.Shared;

namespace KanbanGame.Client.Services;

public interface IEmployeeService
{
    List<Employee> Employees { get; set; }
    Task GetEmployees();
    Task GetEmployeesByTeamId(int teamId);
    Task<Employee?> GetEmployeeById(int employeeId);
    Task CreateEmployee(Employee employee);
    Task UpdateEmployee(int employeeId, Employee employee);
    Task DeleteEmployee(int employeeId);
}
