using Microsoft.AspNetCore.SignalR;

namespace Api.Services;

public class ChatHub : Hub
{
    public ChatHub()
    {
    }
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client Connected at {DateTime.Now} ");
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client Disconnected at {DateTime.Now} ");
        await base.OnDisconnectedAsync(exception);
    }
}