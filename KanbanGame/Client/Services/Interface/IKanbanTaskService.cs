using KanbanGame.Shared;

namespace KanbanGame.Client.Services;

public interface IKanbanTaskService
{
    List<KanbanTask> KanbanTasks { get; set; }
    Task GetKanbanTasks();
    Task GetKanbanTasksByTeamId(int teamId);
    Task<KanbanTask?> GetKanbanTaskById(int kanbanTaskId);
    Task CreateKanbanTask(KanbanTask kanbanTask);
    Task UpdateKanbanTask(int kanbanTaskId, KanbanTask kanbanTask);
    Task DeleteKanbanTask(int kanbanTaskId);
}
