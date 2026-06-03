using Microsoft.VisualStudio.TextTemplating;

namespace Api.DTOs;
public class SendMessageDto
{
    public Guid MatchId {get;set;}
    public string MessageBody {get;set;} = string.Empty;
}