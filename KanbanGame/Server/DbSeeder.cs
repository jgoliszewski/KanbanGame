using KanbanGame.Shared;
using KanbanGame.Server.Services;

namespace KanbanGame.Server.Seeder;

public class DbSeeder
{

    private readonly IEmployeeService _EmployeeService;
    private readonly IKanbanTaskService _KanbanTaskService;
    public DbSeeder(IEmployeeService employeeService, IKanbanTaskService kanbanTaskService)
    {
        _EmployeeService = employeeService;
        _KanbanTaskService = kanbanTaskService;
    }

    private List<Employee> Employees = new List<Employee>(){
        new Employee(){
            Id = 0,
            Name = "John",
            Seniority = Employee.EmployeeSeniority.Junior,
            Status = Employee.EmployeeStatus.Working
        },
        new Employee(){
            Id = 1,
            Name = "Tom",
            Seniority = Employee.EmployeeSeniority.Senior,
            Status = Employee.EmployeeStatus.Working
        },
        new Employee(){
            Id = 2,
            Name = "Alice",
            Seniority = Employee.EmployeeSeniority.Junior,
            Status = Employee.EmployeeStatus.Learning
        },
        new Employee(){
            Id = 3,
            Name = "Margaret",
            Seniority = Employee.EmployeeSeniority.Mid,
            Status = Employee.EmployeeStatus.Working
        },
        new Employee(){
            Id = 4,
            Name = "Mark",
            Seniority = Employee.EmployeeSeniority.Junior,
            Status = Employee.EmployeeStatus.NotWorking
        },
    };
    private List<KanbanTask> KanbanTasks = new List<KanbanTask>(){
            new KanbanTask{
                Title = "Task nr0",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.Waiting,
            },
            new KanbanTask{
                Title = "Task nr1",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.Waiting
            },
            new KanbanTask{
                Title = "Task nr2",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.Waiting
            },
            new KanbanTask{
                Title = "Task nr3",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.Doing
            },
            new KanbanTask{
                Title = "Task nr4",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.Done
            },
        };



    public void Seed()
    {
        KanbanTasks[0].AddAssignee(Employees[0].Id, Employees[0]);
        KanbanTasks[1].AddAssignee(Employees[1].Id, Employees[1]);
        KanbanTasks[2].AddAssignee(Employees[2].Id, Employees[2]);
        KanbanTasks[3].AddAssignee(Employees[3].Id, Employees[3]);
        KanbanTasks[4].AddAssignee(Employees[4].Id, Employees[4]);

        foreach (var task in KanbanTasks)
        {
            _KanbanTaskService.CreateKanbanTask(task);
        }

        foreach (var employee in Employees)
        {
            _EmployeeService.CreateEmployee(employee);
        }
    }
}