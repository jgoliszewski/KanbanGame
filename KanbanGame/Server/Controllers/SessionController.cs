using KanbanGame.Shared;
using KanbanGame.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanGame.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpGet("getSessionInfo")]
    public async Task<Session> GetSessionInfo()
    {
        return await _sessionService.GetSessionInfo();
    }

    [HttpPut]
    public async Task UpdateSessionInfo(Session session)
    {
        await _sessionService.UpdateSession(session);
    }

    [HttpPost("increaseCurrentDay")]
    public async Task IncreaseCurrentDay()
    {
        await _sessionService.IncreaseCurrentDay();
    }

    [HttpPost("simulateDay")]
    public async Task SimulateDay()
    {
        await _sessionService.SimulateDay();
    }
}
