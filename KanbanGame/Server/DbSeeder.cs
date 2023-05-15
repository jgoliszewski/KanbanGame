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
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Cthulhu.png",
            Roles = new Role()
            {
                Seniority = Role.EmployeeSeniority.Mid,
                CurrentRole = Role.EmployeeRole.Analyzer,
                PreviousRole = Role.EmployeeRole.Analyzer,
                IsHighLevelAnalyzer = true,
                IsAnalyzer = true,
                IsDeveloper = null,
                IsTester = false
            }
        },
        new Employee()
        {
            Name = "Olivia",
            Productivity = 0.9,
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Witch.png",
            Roles = new Role()
            {
                Seniority = Role.EmployeeSeniority.Senior,
                CurrentRole = Role.EmployeeRole.Developer,
                PreviousRole = Role.EmployeeRole.Developer,
                IsHighLevelAnalyzer = false,
                IsAnalyzer = false,
                IsDeveloper = true,
                IsTester = false
            }
        },
        new Employee()
        {
            Name = "John",
            Productivity = 0.25,
            Team = Team.TeamName.BackEnd,
            AvatarPath = "Avatars/Yeti.png",
            Roles = new Role()
            {
                Seniority = Role.EmployeeSeniority.Junior,
                CurrentRole = Role.EmployeeRole.Tester,
                PreviousRole = Role.EmployeeRole.Tester,
                IsHighLevelAnalyzer = null,
                IsAnalyzer = null,
                IsDeveloper = false,
                IsTester = true
            }
        },
        new Employee()
        {
            Name = "Emma",
            Productivity = 0.65,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Medusa.png",
            Roles = new Role()
            {
                Seniority = Role.EmployeeSeniority.Mid,
                CurrentRole = Role.EmployeeRole.Analyzer,
                PreviousRole = Role.EmployeeRole.Analyzer,
                IsHighLevelAnalyzer = true,
                IsAnalyzer = true,
                IsDeveloper = null,
                IsTester = false
            }
        },
        new Employee()
        {
            Name = "Arthur",
            Productivity = 0.95,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Mummy.png",
            Roles = new Role()
            {
                Seniority = Role.EmployeeSeniority.Senior,
                CurrentRole = Role.EmployeeRole.Developer,
                PreviousRole = Role.EmployeeRole.Developer,
                IsHighLevelAnalyzer = false,
                IsAnalyzer = false,
                IsDeveloper = true,
                IsTester = false
            }
        },
        new Employee()
        {
            Name = "Sarah",
            Productivity = 0.25,
            Team = Team.TeamName.FrontEnd,
            AvatarPath = "Avatars/Wednesday.png",
            Roles = new Role()
            {
                Seniority = Role.EmployeeSeniority.Junior,
                CurrentRole = Role.EmployeeRole.Tester,
                PreviousRole = Role.EmployeeRole.Tester,
                IsHighLevelAnalyzer = null,
                IsAnalyzer = null,
                IsDeveloper = false,
                IsTester = true
            }
        },
        new Employee()
        {
            Name = "Andy",
            Productivity = 0.65,
            Team = Team.TeamName.HighLevelAnalysis,
            AvatarPath = "Avatars/Reaper.png",
            Roles = new Role()
            {
                Seniority = Role.EmployeeSeniority.Mid,
                CurrentRole = Role.EmployeeRole.HighLevelAnalyzer1,
                PreviousRole = Role.EmployeeRole.HighLevelAnalyzer1,
                IsHighLevelAnalyzer = true,
                IsAnalyzer = null,
                IsDeveloper = null,
                IsTester = null
            }
        },
        new Employee()
        {
            Name = "Hannah",
            Productivity = 0.60,
            Team = Team.TeamName.HighLevelAnalysis,
            AvatarPath = "Avatars/Devil.png",
            Roles = new Role()
            {
                Seniority = Role.EmployeeSeniority.Mid,
                CurrentRole = Role.EmployeeRole.HighLevelAnalyzer2,
                PreviousRole = Role.EmployeeRole.HighLevelAnalyzer2,
                IsHighLevelAnalyzer = true,
                IsAnalyzer = true,
                IsDeveloper = false,
                IsTester = null
            }
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

        KanbanTasks[1].DependencyTask = KanbanTasks[0];
        KanbanTasks[4].DependencyTask = KanbanTasks[3];
        KanbanTasks[6].DependencyTask = KanbanTasks[5];
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
