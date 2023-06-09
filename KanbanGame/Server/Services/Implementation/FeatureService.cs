using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public class FeatureService : IFeatureService
{
    //todo: extract list to repository class
    public List<Feature> Features = new List<Feature>();

    private int _lastId = 0;
    private readonly IKanbanTaskService _kanbanTaskService;

    public FeatureService(IKanbanTaskService kanbanTaskService)
    {
        _kanbanTaskService = kanbanTaskService;
    }

    public async Task<List<Feature>> GetFeatures()
    {
        return Features;
    }

    public async Task<List<Feature>> GetActiveFeatures()
    {
        return Features.FindAll(
            f => f.Status != Feature.FeatureStatus.None
        // && f.Status != Feature.FeatureStatus.Delivered //For FeatureBoard
        );
    }

    public async Task<List<Feature>?> GetFeaturesByTeamId(int teamId)
    {
        var dbFeatures = Features.FindAll(f => (int)f.Team == teamId);
        return dbFeatures;
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
        foreach (var t in Feature.KanbanTasks)
        {
            await _kanbanTaskService.CreateKanbanTask(t);
        }
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
            dbFeature.Assignee = Feature.Assignee;
            dbFeature.Team = Feature.Team;
            dbFeature.EstimatedMinEarnings = Feature.EstimatedMinEarnings;
            dbFeature.EstimatedMaxEarnings = Feature.EstimatedMaxEarnings;
            dbFeature.Effort = Feature.Effort;
            dbFeature.EffortLeft = Feature.EffortLeft;
            dbFeature.Star = Feature.Star;
            dbFeature.Warning = Feature.Warning;
            dbFeature.Pause = Feature.Pause;
            foreach (var t in Feature.KanbanTasks)
            {
                await _kanbanTaskService.UpdateKanbanTask(t.Id, t);
            }
        }
        return dbFeature;
    }

    public async Task<bool> DeleteFeature(int FeatureId)
    {
        var dbFeature = Features.Where(t => t.Id == FeatureId).FirstOrDefault();
        if (dbFeature is not null)
        {
            foreach (var t in dbFeature.KanbanTasks)
            {
                await _kanbanTaskService.DeleteKanbanTask(t.Id);
            }
            Features.Remove(dbFeature);
            return true;
        }
        return false;
    }

    public async Task SendFeatureTasksToTeams(int FeatureId)
    {
        var dbFeature = Features.Where(t => t.Id == FeatureId).FirstOrDefault();
        if (dbFeature is not null)
        {
            foreach (var t in dbFeature.KanbanTasks)
            {
                switch (t.Type)
                {
                    case KanbanTask.TaskType.FrontEnd:
                        t.Team = Team.TeamName.FrontEnd;
                        break;
                    case KanbanTask.TaskType.BackEnd:
                        t.Team = Team.TeamName.BackEnd;
                        break;
                }
                t.Status = KanbanTask.TaskStatus.Backlog;
                await _kanbanTaskService.UpdateKanbanTask(t.Id, t);
            }
            dbFeature.Team = Team.TeamName.None;
            dbFeature.Status = Feature.FeatureStatus.UnderDevelopment;
        }
    }
}
