using KanbanGame.Shared;

namespace KanbanGame.Client.Services;
public interface IEmployeeService
{
    List<Employee> Employees { get; set; }
    Task GetEmployees();
    Task<Employee?> GetEmployeeById(int employeeId);
    Task CreateEmployee(Employee employee);
    Task UpdateEmployee(int employeeId, Employee employee);
    Task DeleteEmployee(int employeeId);

    //todo: check if this version is better
    // Task GetEmployees();
    // Task<Employee?> GetEmployeeById(int EmployeeId);
    // Task<Employee> CreateEmployee(Employee Employee);
    // Task<Employee?> UpdateEmployee(int EmployeeId, Employee Employee);
    // Task<bool> DeleteEmployee(int EmployeeId);
}

