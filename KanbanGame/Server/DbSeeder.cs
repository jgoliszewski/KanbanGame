using KanbanGame.Shared;
using KanbanGame.Server.Services;

namespace KanbanGame.Server.Seeder;

public class DbSeeder
{
    private readonly IEmployeeService _EmployeeService;
    private readonly IKanbanTaskService _KanbanTaskService;
    private readonly IFeatureService _FeatureService;

    public DbSeeder(
        IEmployeeService employeeService,
        IKanbanTaskService kanbanTaskService,
        IFeatureService featureService
    )
    {
        _EmployeeService = employeeService;
        _KanbanTaskService = kanbanTaskService;
        _FeatureService = featureService;
    }

    private List<Employee> Employees = new List<Employee>()
    {
        new Employee()
        {
            Name = "Tom",
            Seniority = Employee.EmployeeSeniority.Junior,
            CurrentRole = Employee.Role.Analyzer,
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Cthulhu.png"
        },
        new Employee()
        {
            Name = "Olivia",
            Seniority = Employee.EmployeeSeniority.Senior,
            CurrentRole = Employee.Role.Developer,
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Witch.png"
        },
        new Employee()
        {
            Name = "John",
            Seniority = Employee.EmployeeSeniority.Junior,
            CurrentRole = Employee.Role.Tester,
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Yeti.png"
        },
        new Employee()
        {
            Name = "Emma",
            Seniority = Employee.EmployeeSeniority.Mid,
            CurrentRole = Employee.Role.Analyzer,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Medusa.png"
        },
        new Employee()
        {
            Name = "Arthur",
            Seniority = Employee.EmployeeSeniority.Junior,
            CurrentRole = Employee.Role.Developer,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Mummy.png"
        },
        new Employee()
        {
            Name = "Sarah",
            Seniority = Employee.EmployeeSeniority.Junior,
            CurrentRole = Employee.Role.Tester,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Wednesday.png"
        },
        new Employee()
        {
            Name = "Andy",
            Seniority = Employee.EmployeeSeniority.Mid,
            CurrentRole = Employee.Role.HighLevelAnalyzer1,
            Team = Team.TeamName.HighLevelAnalysis,
            AvatarPath = "Avatars/Reaper.png"
        },
        new Employee()
        {
            Name = "Hannah",
            Seniority = Employee.EmployeeSeniority.Mid,
            CurrentRole = Employee.Role.HighLevelAnalyzer2,
            Team = Team.TeamName.HighLevelAnalysis,
            AvatarPath = "Avatars/Devil.png"
        },
    };
    
    private List<KanbanTask> KanbanTasks = new List<KanbanTask>()
    {
        new KanbanTask
        {
            Title = "A1",
            Description = "Task description",
            Type = KanbanTask.TaskType.BackEnd
        },
        new KanbanTask
        {
            Title = "A2",
            Description = "Task description",
            Type = KanbanTask.TaskType.FrontEnd
        },
        new KanbanTask
        {
            Title = "A3",
            Description = "Task description",
            Type = KanbanTask.TaskType.FrontEnd
        },
        new KanbanTask
        {
            Title = "B1",
            Description = "Task description",
            Type = KanbanTask.TaskType.BackEnd
        },
        new KanbanTask
        {
            Title = "B2",
            Description = "Task description",
            Type = KanbanTask.TaskType.FrontEnd
        },
        new KanbanTask
        {
            Title = "C1",
            Description = "Task description",
            Type = KanbanTask.TaskType.BackEnd
        },
        new KanbanTask
        {
            Title = "C2",
            Description = "Task description",
            Type = KanbanTask.TaskType.FrontEnd
        },
    };

    private List<Feature> Features = new List<Feature>()
    {
        new Feature
        {
            Title = "A",
            Description = "Cool Feature about sth",
            EstimatedMinEarnings = 12,
            EstimatedMaxEarnings = 18,
        },
        new Feature
        {
            Title = "B",
            Description = "Nice Feature adding cool stuff",
            EstimatedMinEarnings = 10,
            EstimatedMaxEarnings = 15,
        },
        new Feature
        {
            Title = "C",
            Description = "Amazing things",
            EstimatedMinEarnings = 8,
            EstimatedMaxEarnings = 10,
        },
    };

    public void Seed()
    {
        Features[0].KanbanTasks = new List<KanbanTask>()
        {
            KanbanTasks[0],
            KanbanTasks[1],
            KanbanTasks[2]
        };
        Features[1].KanbanTasks = new List<KanbanTask>() { KanbanTasks[3], KanbanTasks[4] };
        Features[2].KanbanTasks = new List<KanbanTask>() { KanbanTasks[5], KanbanTasks[6] };

        // foreach (var task in KanbanTasks)
        // {
        //     _KanbanTaskService.CreateKanbanTask(task);
        // }

        foreach (var employee in Employees)
        {
            _EmployeeService.CreateEmployee(employee);
        }

        foreach (var feature in Features)
        {
            _FeatureService.CreateFeature(feature);
        }
    }
}
