namespace Api.Entities;

public class Notification : BaseEntity<Guid>
{
    public Guid SenderId {get;private set;}
    public string Title { get; private set; } = string.Empty;
    public string Body { get; private set; } = string.Empty;
    public Severity Severity = Severity.Information;

    public static Notification Create(Guid senderId ,string title, string body, Severity severity = Severity.Information)
    {
        return new Notification
        {
            SenderId = senderId,
            Title = title,
            Body = body,
            Severity = severity
        };
    }
}