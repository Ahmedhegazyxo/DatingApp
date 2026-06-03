using Microsoft.AspNetCore.SignalR;

namespace Api.Services;

public class ChatHub : Hub
{
    public ChatHub()
    {
    }
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}