using Api.Helpers;

namespace Api.Services;
public interface IMembersService
{
    Task<List<MemberView>> GetMembersAsync(PaginationFilter? paginationFilter = null,CancellationToken cancellationToken = default!);
    Task<List<MemberView>> GetMatchesAsync(PaginationFilter? paginationFilter = null,CancellationToken cancellationToken = default!);
}