using KanbanGame.Shared;

namespace KanbanGame.Client.Services;
public interface IFeatureService
{
    List<Feature> Features { get; set; }
    Task GetFeatures();
    Task GetFeaturesByTeamId(int teamId);
    Task<Feature?> GetFeatureById(int FeatureId);
    Task CreateFeature(Feature Feature);
    Task UpdateFeature(int FeatureId, Feature Feature);
    Task DeleteFeature(int FeatureId);
}

