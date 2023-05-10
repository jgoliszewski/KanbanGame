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
    public Session Session { get; set; } = new Session { Day = 1 };

    public async Task SimulateDay()
    {
        await _http.PostAsync("api/session/simulateDay", null);
    }

    public async Task GetSessionInfo()
    {
        var result = await _http.GetFromJsonAsync<Session>("api/session/GetSessionInfo");
        if (result is not null)
            Session = result;
    }

    private async Task IncreaseCurrentDay()
    {
        await _http.PostAsync("api/session/increaseCurrentDay", null);
    }
}
