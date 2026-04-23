using Api.Views;

namespace Api.Services;
public interface IMemberMatchingService
{
    Task<MatchView> LikeAndPossibleMatch(Guid receptorId,CancellationToken cancellationToken = default);
}