namespace Api.DTOs;

public class ChatMessageDto
{
    public Guid SenderId { get; set; }
    public string MessageBody { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
}