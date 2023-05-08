using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class FeatureService : IFeatureService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigationManger;

    public FeatureService(HttpClient http, NavigationManager navigationManger)
    {
        _http = http;
        _navigationManger = navigationManger;
    }

    public List<Feature> Features { get; set; } = new List<Feature>();

    public async Task GetFeatures()
    {
        var result = await _http.GetFromJsonAsync<List<Feature>>("api/Feature");
        if (result is not null)
            Features = result;
    }

    public async Task GetActiveFeatures() // Features at least in Backlog
    {
        var result = await _http.GetFromJsonAsync<List<Feature>>("api/Feature/active");
        if (result is not null)
        {
            Features = result;
        }
    }

    public async Task GetFeaturesByTeamId(int teamId)
    {
        var result = await _http.GetFromJsonAsync<List<Feature>>($"api/Feature/team/{teamId}");
        if (result is not null)
            Features = result;
    }

    public async Task<Feature?> GetFeatureById(int FeatureId)
    {
        var result = await _http.GetAsync($"api/Feature/{FeatureId}");
        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await result.Content.ReadFromJsonAsync<Feature>();
        }
        return null;
    }

    public async Task CreateFeature(Feature Feature)
    {
        await _http.PostAsJsonAsync("api/Feature", Feature);
        _navigationManger.NavigateTo("Features");
    }

    public async Task UpdateFeature(int FeatureId, Feature Feature)
    {
        await _http.PutAsJsonAsync($"api/Feature/{FeatureId}", Feature);
    }

    public async Task DeleteFeature(int FeatureId)
    {
        var result = await _http.DeleteAsync($"api/Feature/{FeatureId}");
        _navigationManger.NavigateTo("Features");
    }
}
