using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public interface IKanbanTaskService
{
    Task<List<KanbanTask>> GetKanbanTasks();
    Task<List<KanbanTask>> GetActiveKanbanTasks();
    Task<List<KanbanTask>?> GetKanbanTasksByTeamId(int teamId);
    Task<KanbanTask?> GetKanbanTaskById(int kanbanTaskId);
    Task<KanbanTask> CreateKanbanTask(KanbanTask kanbanTask);
    Task<KanbanTask?> UpdateKanbanTask(int kanbanTaskId, KanbanTask kanbanTask);
    Task<bool> DeleteKanbanTask(int kanbanTaskId);
}
