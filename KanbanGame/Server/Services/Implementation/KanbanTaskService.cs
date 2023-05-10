using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public class KanbanTaskService : IKanbanTaskService
{
    //todo: extract list to repository class
    public List<KanbanTask> KanbanTasks = new List<KanbanTask>();

    private int _lastId = 0;

    public async Task<List<KanbanTask>> GetKanbanTasks()
    {
        return KanbanTasks;
    }

    public async Task<List<KanbanTask>> GetActiveKanbanTasks()
    {
        return KanbanTasks.FindAll(
            t =>
                t.Status != KanbanTask.TaskStatus.Delivered
                && t.Status != KanbanTask.TaskStatus.None
                && t.Status != KanbanTask.TaskStatus.ReadyForDevelopment
        );
    }

    public async Task<List<KanbanTask>?> GetKanbanTasksByTeamId(int teamId)
    {
        var dbTask = KanbanTasks.FindAll(t => (int)t.Team == teamId);
        return dbTask;
    }

    public async Task<KanbanTask?> GetKanbanTaskById(int kanbanTaskId)
    {
        var dbTask = KanbanTasks.Where(t => t.Id == kanbanTaskId).FirstOrDefault();
        return dbTask;
    }

    public async Task<KanbanTask> CreateKanbanTask(KanbanTask kanbanTask)
    {
        //todo: better unique id method
        kanbanTask.Id = _lastId++;
        KanbanTasks.Add(kanbanTask);
        return kanbanTask;
    }

    public async Task<KanbanTask?> UpdateKanbanTask(int KanbanTaskId, KanbanTask kanbanTask)
    {
        var dbTask = KanbanTasks.Where(t => t.Id == KanbanTaskId).FirstOrDefault();
        if (dbTask is not null)
        {
            //ToDo: change to copy function
            dbTask.Title = kanbanTask.Title;
            dbTask.Description = kanbanTask.Description;
            dbTask.Type = kanbanTask.Type;
            dbTask.Status = kanbanTask.Status;
            dbTask.Assignee = kanbanTask.Assignee;
            dbTask.Team = kanbanTask.Team;
            dbTask.Age = kanbanTask.Age;
            dbTask.Effort = kanbanTask.Effort;
            dbTask.EffortLeft = kanbanTask.EffortLeft;
            dbTask.Star = kanbanTask.Star;
            dbTask.Warning = kanbanTask.Warning;
            dbTask.Pause = kanbanTask.Pause;
            if (kanbanTask.DependencyTask is not null)
            {
                // await UpdateKanbanTask(kanbanTask.DependencyTask.Id, kanbanTask.DependencyTask);
            }
        }
        return dbTask;
    }

    public async Task<bool> DeleteKanbanTask(int kanbanTaskId)
    {
        var dbTask = KanbanTasks.Where(t => t.Id == kanbanTaskId).FirstOrDefault();
        if (dbTask is not null)
        {
            KanbanTasks.Remove(dbTask);
            return true;
        }
        return false;
    }
}
