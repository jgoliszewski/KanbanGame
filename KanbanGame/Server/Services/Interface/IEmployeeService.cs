using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetEmployees();
    Task<List<Employee>> GetActiveEmployees();
    Task<List<Employee>?> GetEmployeesByTeamId(int teamId);
    Task<Employee?> GetEmployeeById(int employeeId);
    Task<Employee> CreateEmployee(Employee employee);
    Task<Employee?> UpdateEmployee(int employeeId, Employee employee);
    Task<bool> DeleteEmployee(int employeeId);
}
