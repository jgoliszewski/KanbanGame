using Microsoft.AspNetCore.SignalR;


namespace KanbanGame.Server.Hubs;

public class EmployeeHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
    public async Task UpdateEmployeeList(int id)
    {
        await Clients.All.SendAsync("EmployeeListUpdated", id);
    }
}
