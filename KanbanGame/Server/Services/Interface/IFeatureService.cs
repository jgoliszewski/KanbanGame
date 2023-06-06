using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public interface IFeatureService
{
    Task<List<Feature>> GetFeatures();
    Task<List<Feature>> GetActiveFeatures();
    Task<List<Feature>> GetFeaturesByTeamId(int teamId);
    Task<Feature?> GetFeatureById(int FeatureId);
    Task<Feature> CreateFeature(Feature Feature);
    Task<Feature?> UpdateFeature(int FeatureId, Feature Feature);
    Task<bool> DeleteFeature(int FeatureId);
    Task SendFeatureTasksToTeams(int FeatureId);
}
