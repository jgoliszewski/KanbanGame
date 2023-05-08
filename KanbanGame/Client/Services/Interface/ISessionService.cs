using KanbanGame.Shared;

namespace KanbanGame.Client.Services;

public interface ISessionService
{
    Task SimulateDay();
}
