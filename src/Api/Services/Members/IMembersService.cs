using Api.Helpers;

namespace Api.Services;
public interface IMembersService
{
    Task<PaginatedResult<MemberView>> GetMembersAsync(PaginationFilter paginationFilter,CancellationToken cancellationToken = default!);
    Task<PaginatedResult<MemberView>> GetMatchesAsync(PaginationFilter paginationFilter,CancellationToken cancellationToken = default!);
}