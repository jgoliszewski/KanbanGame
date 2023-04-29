using KanbanGame.Shared;
using KanbanGame.Server.Services;

namespace KanbanGame.Server.Seeder;

public class DbSeeder
{

    private readonly IEmployeeService _EmployeeService;
    private readonly IKanbanTaskService _KanbanTaskService;
    private readonly IFeatureService _FeatureService;
    
    public DbSeeder(IEmployeeService employeeService, IKanbanTaskService kanbanTaskService, IFeatureService featureService)
    {
        _EmployeeService = employeeService;
        _KanbanTaskService = kanbanTaskService;
        _FeatureService = featureService;
    }

    private List<Employee> Employees = new List<Employee>(){
        new Employee(){
            Id = 0,
            Name = "Tom",
            Seniority = Employee.EmployeeSeniority.Junior,
            Status = Employee.EmployeeStatus.Working,
            CurrentRole = Employee.Role.Analyzer,
            AvatarPath = "Avatars/Cthulhu.png"
        },
        new Employee(){
            Id = 1,
            Name = "Olivia",
            Seniority = Employee.EmployeeSeniority.Senior,
            Status = Employee.EmployeeStatus.Working,
            CurrentRole = Employee.Role.Analyzer,
            AvatarPath = "Avatars/Witch.png"
        },
        new Employee(){
            Id = 2,
            Name = "John",
            Seniority = Employee.EmployeeSeniority.Junior,
            Status = Employee.EmployeeStatus.Learning,
            CurrentRole = Employee.Role.Developer,
            AvatarPath = "Avatars/Yeti.png"
        },
        new Employee(){
            Id = 3,
            Name = "Emma",
            Seniority = Employee.EmployeeSeniority.Mid,
            Status = Employee.EmployeeStatus.Working,
            CurrentRole = Employee.Role.Developer,
            AvatarPath = "Avatars/Medusa.png"
        },
        new Employee(){
            Id = 4,
            Name = "Arthur",
            Seniority = Employee.EmployeeSeniority.Junior,
            Status = Employee.EmployeeStatus.NotWorking,
            CurrentRole = Employee.Role.Tester,
            AvatarPath = "Avatars/Mummy.png"
        },
    };
    private List<KanbanTask> KanbanTasks = new List<KanbanTask>()
    {
            new KanbanTask{
                Title = "A1",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.Backlog,
            },
            new KanbanTask{
                Title = "A2",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.AnalysisDoing
            },
            new KanbanTask{
                Title = "A3",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.DevelopmentWaiting
            },
            new KanbanTask{
                Title = "B1",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.TestWaiting
            },
            new KanbanTask{
                Title = "B2",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.Backlog
            },
            new KanbanTask{
                Title = "C1",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.Backlog,
            },
            new KanbanTask{
                Title = "C2",
                Description = "Task description",
                Status = KanbanTask.TaskStatus.TestDoing,
            },
        };

    private List<Feature> Features = new List<Feature>()
    {
        new Feature{
            Title = "A",
            Description = "Cool Feature about sth",
            Status = Feature.FeatureStatus.Backlog,
        },
        new Feature{
            Title = "B",
            Description = "Nice Feature adding cool stuff",
            Status = Feature.FeatureStatus.Backlog,
        },
        new Feature{
            Title = "C",
            Description = "Amazing things",
            Status = Feature.FeatureStatus.Backlog,
        }
    };

    public void Seed()
    {
        // KanbanTasks[0].AddAssignee(Employees[0]);
        // KanbanTasks[1].AddAssignee(Employees[1]);
        // KanbanTasks[2].AddAssignee(Employees[2]);
        // KanbanTasks[3].AddAssignee(Employees[3]);

        Features[0].KanbanTasks = new List<KanbanTask>(){
            KanbanTasks[0],
            KanbanTasks[1],
            KanbanTasks[2]};
        Features[1].KanbanTasks = new List<KanbanTask>(){
            KanbanTasks[3],
            KanbanTasks[4]};
        Features[2].KanbanTasks = new List<KanbanTask>(){
            KanbanTasks[5],
            KanbanTasks[6]};

        foreach (var task in KanbanTasks)
        {
            _KanbanTaskService.CreateKanbanTask(task);
        }

        foreach (var employee in Employees)
        {
            _EmployeeService.CreateEmployee(employee);
        }

        foreach(var feature in Features)
        {
            _FeatureService.CreateFeature(feature);
        }
    }
}