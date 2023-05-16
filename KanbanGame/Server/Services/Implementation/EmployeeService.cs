using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public class EmployeeService : IEmployeeService
{
    public List<Employee> Employees = new List<Employee>();
    private int _lastId = 0;

    public async Task<List<Employee>> GetEmployees()
    {
        return Employees;
    }

    public async Task<List<Employee>> GetActiveEmployees()
    {
        return Employees.FindAll(e => e.Roles.Status != Role.EmployeeStatus.NotWorking);
    }

    public async Task<List<Employee>?> GetEmployeesByTeamId(int teamId)
    {
        var dbEmployees = Employees.FindAll(t => (int)t.Roles.Team == teamId);
        return dbEmployees;
    }

    public async Task<Employee?> GetEmployeeById(int employeeId)
    {
        var dbEmployee = Employees.Where(t => t.Id == employeeId).FirstOrDefault();
        return dbEmployee;
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        //todo: better unique id method
        employee.Id = _lastId++;
        Employees.Add(employee);
        return employee;
    }

    public async Task<Employee?> UpdateEmployee(int employeeId, Employee employee)
    {
        var dbEmployee = Employees.Where(t => t.Id == employeeId).FirstOrDefault();
        if (dbEmployee is not null)
        {
            //ToDo: change to copy function
            dbEmployee.Name = employee.Name;
            dbEmployee.Roles = employee.Roles;
            dbEmployee.Productivity = employee.Productivity;
            dbEmployee.AvatarPath = employee.AvatarPath;
        }
        return dbEmployee;
    }

    public async Task<bool> DeleteEmployee(int employeeId)
    {
        //todo: remove this emloyee from his tasks
        var dbEmployee = Employees.Where(t => t.Id == employeeId).FirstOrDefault();
        if (dbEmployee is not null)
        {
            Employees.Remove(dbEmployee);
            return true;
        }
        return false;
    }
}
