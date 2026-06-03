using Api.Helpers;
using Api.Repositories;
using SQLitePCL;

namespace Api.Services;

public class ChatMessageService(
    IChatMessageRepository chatMessageRepository,
    INotificationService notificationService,
    IMatchRepository matchRepository,
    ILogger<ChatMessage> logger,
 IUserClaimsService userClaimsService) : IChatMessageService
{
    private readonly IUserClaimsService _userClaimsService = userClaimsService;
    private readonly ILogger<ChatMessage> _logger = logger;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IChatMessageRepository _chatMessageRepository = chatMessageRepository;
    private readonly IMatchRepository _matchRepository = matchRepository;

    public Task<PaginatedResult<ChatMessageDto>> GetPaginatedChatMessages(PaginationFilter paginationFilter, Guid matchId, CancellationToken cancellationToken = default)
    {

        Guid currentUserId = Guid.Parse(
                _userClaimsService.ClaimsPrincipal!
                    .FindFirstValue(ClaimTypes.NameIdentifier)!
            );
        return _chatMessageRepository.ReadAsResult(paginationFilter, e => new ChatMessageDto
        {
            SenderId = e.SenderId,
            MessageBody = e.MessageBody,
            SentAt = e.CreatedDate
        }, cancellationToken, e => e.MessageMatchId == matchId);
    }

    public async Task<bool> SendMessage(SendMessageDto sendMessageDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Guid currentUserId = Guid.Parse(
                   _userClaimsService.ClaimsPrincipal!
                       .FindFirstValue(ClaimTypes.NameIdentifier)!
               );
            PaginatedResult<MatchDto> matchPaginated = await _matchRepository.ReadAsResult(new PaginationFilter
            {
                PageNumber = 1,
                PageSize = 1,
            },
            e => new MatchDto
            {
                AdjacentId = e.CreatorProfileId == currentUserId ? e.ReceptorProfileId : e.CreatorProfileId,
                SenderName = e.CreatorProfileId == currentUserId ? e.CreatorProfile!.FirstName + " " + e.CreatorProfile.LastName :
                e.ReceptorProfile!.FirstName + " " + e.ReceptorProfile.LastName
            },
            cancellationToken,
            e => e.Id == sendMessageDto.MatchId
            );
            MatchDto match = matchPaginated.Body.First();
            ChatMessage chatMessage = ChatMessage.Create(sendMessageDto.MatchId, currentUserId, sendMessageDto.MessageBody);
            await _chatMessageRepository.CreateAsync(chatMessage,
             cancellationToken
            );
            await _notificationService.Send(match.AdjacentId,
             Notification.Create(currentUserId, $"From {match.SenderName}",
             chatMessage.MessageBody,
              Severity.Information));

            return true;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.ToString());
            return false;
        }
    }
}