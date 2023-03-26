using KanbanGame.Shared;
using KanbanGame.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanGame.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KanbanTaskController : ControllerBase
{
    private readonly IKanbanTaskService _TaskService;
    public KanbanTaskController(IKanbanTaskService TaskService)
    {
        _TaskService = TaskService;
    }

    [HttpGet]
    public async Task<List<KanbanTask>> GetTasks()
    {
        return await _TaskService.GetKanbanTasks();
    }

    [HttpGet("{id}")]
    public async Task<KanbanTask?> GetTaskById(int id)
    {
        return await _TaskService.GetKanbanTaskById(id);
    }

    [HttpPost]
    public async Task<KanbanTask?> CreateTask(KanbanTask task)
    {
        return await _TaskService.CreateKanbanTask(task);
    }

    [HttpPut("{id}")]
    public async Task<KanbanTask?> UpdateTask(int id, KanbanTask task)
    {
        return await _TaskService.UpdateKanbanTask(id, task);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteTask(int id)
    {
        return await _TaskService.DeleteKanbanTask(id);
    }
}