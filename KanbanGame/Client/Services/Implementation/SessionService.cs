using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class SessionService : ISessionService
{
    private readonly IKanbanTaskService _kanbanTaskService;
    private readonly IEmployeeService _employeeService;
    private readonly IFeatureService _featureService;
    private readonly HttpClient _http;

    public SessionService(
        HttpClient http,
        IKanbanTaskService kanbanTaskService,
        IEmployeeService employeeService,
        IFeatureService featureService
    )
    {
        _http = http;
        _kanbanTaskService = kanbanTaskService;
        _employeeService = employeeService;
        _featureService = featureService;
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
    }

    private async Task UpdateEmployeesLocally()
    {
        await _employeeService.GetActiveEmployees();

        foreach (var e in _employeeService.Employees) { }
    }

    private async Task UpdateKanbanTasksLocally()
    {
        await _kanbanTaskService.GetActiveKanbanTasks();
        foreach (var t in _kanbanTaskService.KanbanTasks)
        {
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
            KanbanTasksToUpdate.Add(t);
        }
    }

    private async Task UpdateFeaturesLocally()
    {
        await _featureService.GetActiveFeatures();
        foreach (var f in _featureService.Features)
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
