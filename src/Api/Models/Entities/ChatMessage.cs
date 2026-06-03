namespace Api.Entities;

public class ChatMessage : BaseEntity<Guid>
{
    public virtual ProfileMatch? MessageMatch { get; private set; }
    public Guid MessageMatchId { get; private set; }
    public string MessageBody { get; private set; } = string.Empty;
    public Guid SenderId { get; private set; }
    public virtual Profile? SenderProfile {get;private set;}
    public static ChatMessage Create(Guid matchId, Guid senderId,string messageBody)
    {
        return new ChatMessage
        {
            SenderId = senderId,
            MessageMatchId = matchId,
            MessageBody = messageBody
        };
    }
}