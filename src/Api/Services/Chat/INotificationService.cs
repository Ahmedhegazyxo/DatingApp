namespace Api.Services;

public interface INotificationService
{
    Task Send(Guid userId, Notification notification, string eventName = "RecievedMessage");
}