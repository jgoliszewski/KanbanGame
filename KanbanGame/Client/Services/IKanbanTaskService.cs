using KanbanGame.Shared;

namespace KanbanGame.Client.Services;
public interface IKanbanTaskService
{
    List<KanbanTask> KanbanTasks { get; set; }
    Task GetKanbanTasks();
    Task<KanbanTask?> GetKanbanTaskById(int kanbanTaskId);
    Task CreateKanbanTask(KanbanTask kanbanTask);
    Task UpdateKanbanTask(int kanbanTaskId, KanbanTask kanbanTask);
    Task DeleteKanbanTask(int kanbanTaskId);

    //todo: check if this version is better
    // Task GetKanbanTasks();
    // Task<KanbanTask?> GetKanbanTaskById(int KanbanTaskId);
    // Task<KanbanTask> CreateKanbanTask(KanbanTask KanbanTask);
    // Task<KanbanTask?> UpdateKanbanTask(int KanbanTaskId, KanbanTask KanbanTask);
    // Task<bool> DeleteKanbanTask(int KanbanTaskId);
}

