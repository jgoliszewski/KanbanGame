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
            Productivity = 0.6,
            Seniority = Employee.EmployeeSeniority.Mid,
            CurrentRole = Employee.Role.Analyzer,
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Cthulhu.png"
        },
        new Employee()
        {
            Name = "Olivia",
            Productivity = 0.9,
            Seniority = Employee.EmployeeSeniority.Senior,
            CurrentRole = Employee.Role.Developer,
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Witch.png"
        },
        new Employee()
        {
            Name = "John",
            Productivity = 0.25,
            Seniority = Employee.EmployeeSeniority.Junior,
            CurrentRole = Employee.Role.Tester,
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Yeti.png"
        },
        new Employee()
        {
            Name = "Emma",
            Productivity = 0.65,
            Seniority = Employee.EmployeeSeniority.Mid,
            CurrentRole = Employee.Role.Analyzer,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Medusa.png"
        },
        new Employee()
        {
            Name = "Arthur",
            Productivity = 0.95,
            Seniority = Employee.EmployeeSeniority.Senior,
            CurrentRole = Employee.Role.Developer,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Mummy.png"
        },
        new Employee()
        {
            Name = "Sarah",
            Productivity = 0.25,
            Seniority = Employee.EmployeeSeniority.Junior,
            CurrentRole = Employee.Role.Tester,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Wednesday.png"
        },
        new Employee()
        {
            Name = "Andy",
            Productivity = 0.65,
            Seniority = Employee.EmployeeSeniority.Mid,
            CurrentRole = Employee.Role.HighLevelAnalyzer1,
            Team = Team.TeamName.HighLevelAnalysis,
            AvatarPath = "Avatars/Reaper.png"
        },
        new Employee()
        {
            Name = "Hannah",
            Productivity = 0.60,
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
            Effort = 0.5,
            Description = "Task description",
            Type = KanbanTask.TaskType.BackEnd
        },
        new KanbanTask
        {
            Title = "A2",
            Effort = 0.5,
            Description = "Task description",
            Type = KanbanTask.TaskType.FrontEnd
        },
        new KanbanTask
        {
            Title = "A3",
            Effort = 0.6,
            Description = "Task description",
            Type = KanbanTask.TaskType.FrontEnd
        },
        new KanbanTask
        {
            Title = "B1",
            Effort = 0.3,
            Description = "Task description",
            Type = KanbanTask.TaskType.BackEnd
        },
        new KanbanTask
        {
            Title = "B2",
            Effort = 0.6,
            Description = "Task description",
            Type = KanbanTask.TaskType.FrontEnd
        },
        new KanbanTask
        {
            Title = "C1",
            Effort = 0.7,
            Description = "Task description",
            Type = KanbanTask.TaskType.BackEnd
        },
        new KanbanTask
        {
            Title = "C2",
            Effort = 0.4,
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

        KanbanTasks[0].DependencyTask = KanbanTasks[2];
        KanbanTasks[3].DependencyTask = KanbanTasks[4];
        KanbanTasks[5].DependencyTask = KanbanTasks[6];
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
