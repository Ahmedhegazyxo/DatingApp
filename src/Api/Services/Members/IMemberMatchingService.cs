using Api.Views;

namespace Api.Services;
public interface IMemberMatchingService
{
    Task<LikeResponseView> LikeAndPossibleMatch(Guid receptorId,CancellationToken cancellationToken = default);
}