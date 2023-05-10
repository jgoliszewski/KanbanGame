using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public interface ISessionService
{
    Task<Session> GetSessionInfo();
    Task IncreaseCurrentDay();
    Task SimulateDay();
}
