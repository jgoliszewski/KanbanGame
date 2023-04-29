using KanbanGame.Shared;

namespace KanbanGame.Server.Services;
public interface IFeatureService
{
    Task<List<Feature>> GetFeatures();
    Task<Feature?> GetFeatureById(int FeatureId);
    Task<Feature> CreateFeature(Feature Feature);
    Task<Feature?> UpdateFeature(int FeatureId, Feature Feature);
    Task<bool> DeleteFeature(int FeatureId);
}

