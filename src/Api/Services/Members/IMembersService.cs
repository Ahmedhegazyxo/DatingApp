using Api.Helpers;

namespace Api.Services;
public interface IMembersService
{
    Task<List<MemberView>> GetMembersAsync(PaginationFilter? paginationFilter = null,CancellationToken cancellationToken = default!);
}