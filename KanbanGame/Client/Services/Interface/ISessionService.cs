using KanbanGame.Shared;

namespace KanbanGame.Client.Services;

public interface ISessionService
{
    Task SimulateDay();
    Task GetSessionInfo();
    Task UpdateSessionInfo(Session session);
    Session Session { get; set; }
}
