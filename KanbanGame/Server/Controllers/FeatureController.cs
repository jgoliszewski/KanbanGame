using KanbanGame.Shared;
using KanbanGame.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanGame.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeatureController : ControllerBase
{
    private readonly IFeatureService _FeatureService;

    public FeatureController(IFeatureService FeatureService)
    {
        _FeatureService = FeatureService;
    }

    [HttpGet]
    public async Task<List<Feature>> GetFeatures()
    {
        return await _FeatureService.GetFeatures();
    }

    [HttpGet("active")]
    public async Task<List<Feature>> GetActiveFeatures()
    {
        return await _FeatureService.GetActiveFeatures();
    }

    [HttpGet("team/{id}")]
    public async Task<List<Feature>?> GetFeatureByTeamId(int id)
    {
        return await _FeatureService.GetFeaturesByTeamId(id);
    }

    [HttpGet("{id}")]
    public async Task<Feature?> GetFeatureById(int id)
    {
        return await _FeatureService.GetFeatureById(id);
    }

    [HttpPost]
    public async Task<Feature?> CreateFeature(Feature Feature)
    {
        return await _FeatureService.CreateFeature(Feature);
    }

    [HttpPut("{id}")]
    public async Task<Feature?> UpdateFeature(int id, Feature Feature)
    {
        return await _FeatureService.UpdateFeature(id, Feature);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteFeature(int id)
    {
        return await _FeatureService.DeleteFeature(id);
    }
}
