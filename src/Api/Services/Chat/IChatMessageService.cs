using Api.Helpers;

namespace Api.Services;
public interface IChatMessageService
{
    Task<PaginatedResult<ChatMessageDto>> GetPaginatedChatMessages(PaginationFilter paginationFilter,Guid matchId, CancellationToken cancellationToken = default);
    Task<bool> SendMessage(SendMessageDto sendMessageDto, CancellationToken cancellationToken = default);
}