using Microsoft.AspNetCore.SignalR;


namespace KanbanGame.Server.Hubs;

public class BoardHub : Hub
{
    public static List<string> ConnectedUsersIds = new List<string>();
    public static List<string> ReadyUsersIds = new List<string>();
    
    public override async Task OnConnectedAsync()
    {
        ConnectedUsersIds.Add(Context.ConnectionId); 

        await Clients.All.SendAsync("UserCount", ConnectedUsersIds.Count);
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception ex)
    {
        ConnectedUsersIds.Remove(Context.ConnectionId); 
        ReadyUsersIds.Remove(Context.ConnectionId); 

        await Clients.All.SendAsync("UserCount", ConnectedUsersIds.Count);
        await Clients.All.SendAsync("StateReady", ReadyUsersIds.Count);
        await base.OnDisconnectedAsync(ex);
    }
    public async Task UpdateBoard(int boardId)
    {
        await Clients.All.SendAsync("BoardUpdated", boardId);
    }
    public async Task StateReady(int _)
    {
        if(!ReadyUsersIds.Contains(Context.ConnectionId))
        {
            ReadyUsersIds.Add(Context.ConnectionId); 
        }
        await Clients.All.SendAsync("StateReady", ReadyUsersIds.Count);
    }
    public async Task StateNotReady(int _)
    {
        if(ReadyUsersIds.Contains(Context.ConnectionId))
        {
            ReadyUsersIds.Remove(Context.ConnectionId); 
        }
        await Clients.All.SendAsync("StateNotReady", ReadyUsersIds.Count);
    }

    public async Task ClearReadyUsers(int _)
    {
        ReadyUsersIds.Clear();
        await Clients.All.SendAsync("ClearReadyUsers", ReadyUsersIds.Count);
    }

}
