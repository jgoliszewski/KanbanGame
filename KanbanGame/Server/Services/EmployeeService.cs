using KanbanGame.Shared;

namespace KanbanGame.Server.Services;
public class EmployeeService : IEmployeeService
{
    public List<Employee> Employees = new List<Employee>{
        new Employee(0, "John", Employee.EmployeeSeniority.Junior, Employee.EmployeeStatus.Working),
        new Employee(1, "Alice", Employee.EmployeeSeniority.Senior, Employee.EmployeeStatus.Working),
        new Employee(2, "Jacob", Employee.EmployeeSeniority.Junior, Employee.EmployeeStatus.Learning),
        new Employee(3, "Tom", Employee.EmployeeSeniority.Mid, Employee.EmployeeStatus.NotWorking),
    };
    private int _lastId = 3;
    public async Task<List<Employee>> GetEmployees()
    {
        return Employees;
    }

    public async Task<Employee?> GetEmployeeById(int employeeId)
    {
        var dbEmployee = Employees.Where(t => t.Id == employeeId).FirstOrDefault();
        return dbEmployee;
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        //todo: better unique id method
        employee.Id = ++_lastId;
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
            dbEmployee.Seniority = employee.Seniority;
            dbEmployee.Status = employee.Status;
        }
        return dbEmployee;
    }

    public async Task<bool> DeleteEmployee(int employeeId)
    {
        var dbEmployee = Employees.Where(t => t.Id == employeeId).FirstOrDefault();
        if (dbEmployee is not null)
        {
            Employees.Remove(dbEmployee);
            return true;
        }
        return false;
    }
}