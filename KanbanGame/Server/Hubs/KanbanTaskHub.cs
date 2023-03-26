using Microsoft.AspNetCore.SignalR;


namespace KanbanGame.Server.Hubs;

public class KanbanTaskHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
    public async Task UpdateKanbanTaskList(int id)
    {
        await Clients.All.SendAsync("KanbanTaskListUpdated", id);
    }
}
