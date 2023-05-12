using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public class SessionService : ISessionService
{
    private readonly IKanbanTaskService _kanbanTaskService;
    private readonly IEmployeeService _employeeService;
    private readonly IFeatureService _featureService;

    public SessionService(
        IKanbanTaskService kanbanTaskService,
        IEmployeeService employeeService,
        IFeatureService featureService
    )
    {
        _kanbanTaskService = kanbanTaskService;
        _employeeService = employeeService;
        _featureService = featureService;
    }

    public Session CurrentSession = new Session { Day = 1 };

    public async Task<Session> GetSessionInfo()
    {
        return CurrentSession;
    }

    public async Task IncreaseCurrentDay()
    {
        if (CurrentSession.Day % 7 == 5)
        {
            CurrentSession.Day += 3;
        }
        else
        {
            CurrentSession.Day++;
        }
    }

    private List<Employee> EmployeesToUpdate;
    private List<KanbanTask> KanbanTasksToUpdate;
    private List<Feature> FeaturesToUpdate;

    public async Task SimulateDay()
    {
        EmployeesToUpdate = new List<Employee>();
        KanbanTasksToUpdate = new List<KanbanTask>();
        FeaturesToUpdate = new List<Feature>();
        await UpdateEmployeesLocally();
        await UpdateFeaturesLocally();
        await UpdateKanbanTasksLocally();

        await UpdateEntities();
        await IncreaseCurrentDay();
    }

    private async Task UpdateEmployeesLocally()
    {
        var EmployeesToUpdate = await _employeeService.GetActiveEmployees();
        foreach (var e in EmployeesToUpdate)
        {
            if (e.Roles.Status == Role.EmployeeStatus.Learning)
            {
                e.Roles.LearningDaysLeft--;
                if (e.Roles.LearningDaysLeft <= 0)
                {
                    e.Roles.Status = Role.EmployeeStatus.Working;
                    switch (e.Roles.CurrentRole)
                    {
                        case Role.EmployeeRole.Analyzer:
                            e.Roles.IsAnalyzer = true;
                            break;
                        case Role.EmployeeRole.Developer:
                            e.Roles.IsDeveloper = true;
                            break;
                        case Role.EmployeeRole.Tester:
                            e.Roles.IsTester = true;
                            break;
                        case Role.EmployeeRole.HighLevelAnalyzer1:
                        case Role.EmployeeRole.HighLevelAnalyzer2:
                            e.Roles.IsHighLevelAnalyzer = true;
                            break;
                    }
                }
            }

            if (e.Roles.Status == Role.EmployeeStatus.Transitioning)
            {
                e.Roles.TransitioningDaysLeft--;
                if (e.Roles.TransitioningDaysLeft <= 0)
                {
                    e.Roles.Status = Role.EmployeeStatus.Working;
                }
            }
        }
    }

    private async Task UpdateKanbanTasksLocally()
    {
        KanbanTasksToUpdate = await _kanbanTaskService.GetActiveKanbanTasks();
        foreach (var t in KanbanTasksToUpdate)
        {
            if (CurrentSession.Day % 7 == 5)
                t.Age += 3;
            else
                t.Age++;

            if (t.Assignee is not null)
            {
                t.EffortLeft -= t.Assignee.Productivity;
                if (t.EffortLeft <= 0)
                {
                    t.EffortLeft = t.Effort;
                    t.NextTaskStatus();
                    t.Assignee = null;
                }
            }
        }
    }

    private async Task UpdateFeaturesLocally()
    {
        var activeFeatures = await _featureService.GetActiveFeatures();
        foreach (var f in activeFeatures)
        {
            if (f.Assignee is not null)
            {
                f.EffortLeft -= f.Assignee.Productivity;
                if (f.EffortLeft <= 0)
                {
                    f.EffortLeft = f.Effort;
                    f.NextFeatureStatus();
                    f.Assignee = null;
                }
                FeaturesToUpdate.Add(f);
            }
        }
    }

    private async Task UpdateEntities()
    {
        foreach (var e in EmployeesToUpdate)
            await _employeeService.UpdateEmployee(e.Id, e);

        foreach (var f in FeaturesToUpdate)
            await _featureService.UpdateFeature(f.Id, f);

        foreach (var t in KanbanTasksToUpdate)
            await _kanbanTaskService.UpdateKanbanTask(t.Id, t);
    }
}
