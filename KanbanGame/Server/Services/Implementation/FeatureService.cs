using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public class FeatureService : IFeatureService
{
    //todo: extract list to repository class
    public List<Feature> Features = new List<Feature>();

    private int _lastId = 0;
    public async Task<List<Feature>> GetFeatures()
    {
        return Features;
    }
    public async Task<Feature?> GetFeatureById(int FeatureId)
    {
        var dbFeature = Features.Where(t => t.Id == FeatureId).FirstOrDefault();
        return dbFeature;
    }
    public async Task<Feature> CreateFeature(Feature Feature)
    {
        //todo: better unique id method
        Feature.Id = _lastId++;
        Features.Add(Feature);
        return Feature;
    }

    public async Task<Feature?> UpdateFeature(int FeatureId, Feature Feature)
    {
        var dbFeature = Features.Where(t => t.Id == FeatureId).FirstOrDefault();
        if (dbFeature is not null)
        {
            //ToDo: change to copy function
            dbFeature.Title = Feature.Title;
            dbFeature.Description = Feature.Description;
            dbFeature.Status = Feature.Status;
            dbFeature.KanbanTasks = Feature.KanbanTasks;
        }
        return dbFeature;
    }
    public async Task<bool> DeleteFeature(int FeatureId)
    {
        var dbFeature = Features.Where(t => t.Id == FeatureId).FirstOrDefault();
        if (dbFeature is not null)
        {
            Features.Remove(dbFeature);
            return true;
        }
        return false;
    }
}