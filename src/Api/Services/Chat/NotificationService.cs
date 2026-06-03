using Microsoft.AspNetCore.SignalR;

namespace Api.Services;

public class NotificationService : INotificationService
{
    IHubContext<ChatHub> _hub;
    public NotificationService(IHubContext<ChatHub> hub)
    {
        _hub = hub;
    }
    public async Task Send(Guid userId, Notification notification, string eventName = "RecievedMessage")
    {
        await _hub.Clients.User(userId.ToString()).SendAsync(eventName, notification);
    }
}