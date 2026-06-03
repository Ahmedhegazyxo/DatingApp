using Api.Helpers;
using Api.Views;

namespace Api.Services;

public interface IMatchService
{
    Task<PaginatedResult<MemberView>> GetMatchesAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken = default!);
}