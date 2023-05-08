using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class KanbanTaskService : IKanbanTaskService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigationManger;

    public KanbanTaskService(HttpClient http, NavigationManager navigationManger)
    {
        _http = http;
        _navigationManger = navigationManger;
    }

    public List<KanbanTask> KanbanTasks { get; set; } = new List<KanbanTask>();

    public async Task GetKanbanTasks()
    {
        var result = await _http.GetFromJsonAsync<List<KanbanTask>>("api/kanbanTask");
        if (result is not null)
            KanbanTasks = result;
    }

    public async Task GetActiveKanbanTasks()
    {
        var result = await _http.GetFromJsonAsync<List<KanbanTask>>("api/kanbanTask/active");
        if (result is not null)
            KanbanTasks = result;
    }

    public async Task GetKanbanTasksByTeamId(int teamId)
    {
        var result = await _http.GetFromJsonAsync<List<KanbanTask>>(
            $"api/kanbanTask/team/{teamId}"
        );
        if (result is not null)
            KanbanTasks = result;
    }

    public async Task<KanbanTask?> GetKanbanTaskById(int kanbanTaskId)
    {
        var result = await _http.GetAsync($"api/kanbanTask/{kanbanTaskId}");
        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await result.Content.ReadFromJsonAsync<KanbanTask>();
        }
        return null;
    }

    public async Task CreateKanbanTask(KanbanTask kanbanTask)
    {
        await _http.PostAsJsonAsync("api/kanbanTask", kanbanTask);
    }

    public async Task UpdateKanbanTask(int kanbanTaskId, KanbanTask kanbanTask)
    {
        await _http.PutAsJsonAsync($"api/kanbanTask/{kanbanTaskId}", kanbanTask);
    }

    public async Task DeleteKanbanTask(int kanbanTaskId)
    {
        var result = await _http.DeleteAsync($"api/kanbanTask/{kanbanTaskId}");
    }
}
