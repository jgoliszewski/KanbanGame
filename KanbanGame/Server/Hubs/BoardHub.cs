using Microsoft.AspNetCore.SignalR;


namespace KanbanGame.Server.Hubs;

public class BoardHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
    public async Task UpdateBoard(int boardId)
    {
        await Clients.All.SendAsync("BoardUpdated", boardId);
    }
}
