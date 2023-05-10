using KanbanGame.Shared;

namespace KanbanGame.Server.Services;

public class SessionService : ISessionService
{
    public Session CurrentSession = new Session { Day = 1 };

    public async Task<Session> GetSessionInfo()
    {
        return CurrentSession;
    }

    public async Task IncreaseCurrentDay()
    {
        if (CurrentSession.Day % 7 == 5)
        {
            CurrentSession.Day += 3;
        }
        else
        {
            CurrentSession.Day++;
        }
    }

    public async Task SimulateDay()
    {
        throw new NotImplementedException();
    }
}
