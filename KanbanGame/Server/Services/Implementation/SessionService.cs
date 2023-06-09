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

    public async Task UpdateSession(Session session)
    {
        CurrentSession = session;
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
            e.Roles.PreviousRole = e.Roles.CurrentRole;
            e.Roles.PreviousTeam = e.Roles.Team;
            if (e.Roles.Status == Role.EmployeeStatus.Transitioning)
            {
                e.Roles.TransitioningDaysLeft--;
                e.Roles.IsBlocked = true;
                if (e.Roles.TransitioningDaysLeft <= 0)
                {
                    if (e.Roles.LearningDaysLeft > 0)
                    {
                        e.Roles.Status = Role.EmployeeStatus.Learning;
                    }
                    else
                    {
                        e.Roles.Status = Role.EmployeeStatus.Working;
                        e.Roles.IsBlocked = false;
                    }
                }
            }
            else if (e.Roles.Status == Role.EmployeeStatus.Learning)
            {
                e.Roles.LearningDaysLeft--;
                e.Roles.IsBlocked = true;
                if (e.Roles.LearningDaysLeft <= 0)
                {
                    e.Roles.IsBlocked = false;
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

                        case Role.EmployeeRole.HighLevelAnalyzer2:
                        case Role.EmployeeRole.HighLevelAnalyzer1:
                            e.Roles.IsHighLevelAnalyzer = true;
                            break;
                    }
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

            if (t.Assignee is not null && t.Assignee.Roles.Status == Role.EmployeeStatus.Working)
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

                    if (
                        f.Status == Feature.FeatureStatus.ReadyForDevelopment
                        && !CurrentSession.IsFeatureExtraColumnUnlocked
                    )
                    {
                        await _featureService.SendFeatureTasksToTeams(f.Id);
                        f.NextFeatureStatus();
                    }
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
