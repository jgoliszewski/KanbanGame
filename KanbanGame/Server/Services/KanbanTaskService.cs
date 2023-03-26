using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public class KanbanTaskService : IKanbanTaskService
{
    //todo: extract list to repository class
    public List<KanbanTask> KanbanTasks = new List<KanbanTask>{
        new KanbanTask(0, "Task_0", null, KanbanTask.TaskStatus.Waiting),
        new KanbanTask(1, "Task_1", null, KanbanTask.TaskStatus.Waiting),
        new KanbanTask(2, "Task_2", null, KanbanTask.TaskStatus.Waiting),
        new KanbanTask(3, "Task_3", null, KanbanTask.TaskStatus.Waiting),
    };
    public async Task<List<KanbanTask>> GetKanbanTasks()
    {
        return KanbanTasks;
    }
    public async Task<KanbanTask?> GetKanbanTaskById(int kanbanTaskId)
    {
        var dbTask = KanbanTasks.Where(t => t.Id == kanbanTaskId).FirstOrDefault();
        return dbTask;
    }
    public async Task<KanbanTask> CreateKanbanTask(KanbanTask kanbanTask)
    {
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
            dbTask.Status = kanbanTask.Status;
            dbTask.EmployeeId = kanbanTask.EmployeeId;
            dbTask.Employee = kanbanTask.Employee;
            dbTask.PpEmployeeId = kanbanTask.PpEmployeeId;
            dbTask.PpEmployee = kanbanTask.PpEmployee;
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