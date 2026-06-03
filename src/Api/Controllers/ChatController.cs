using Api.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatMessageService _chatMessageService;
    private readonly IHubContext<ChatHub> _hub;
    public ChatController(IChatMessageService chatMessageService, IHubContext<ChatHub> hub)
    {
        _chatMessageService = chatMessageService;
        _hub = hub;
    }
    [HttpGet("{matchId}/messages")]
    public async Task<IActionResult> ChatMatchMessages([FromQuery] PaginationFilter paginationFilter, Guid matchId, CancellationToken cancellationToken = default)
    {
        return Ok(await _chatMessageService.GetPaginatedChatMessages(paginationFilter, matchId, cancellationToken));
    }
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto sendMessageDto, CancellationToken cancellationToken)
    {
        bool isSent = await _chatMessageService.SendMessage(sendMessageDto, cancellationToken);
        return Ok(new
        {
            IsSent = isSent,
            TimeSent = DateTime.UtcNow
        });
    }
}