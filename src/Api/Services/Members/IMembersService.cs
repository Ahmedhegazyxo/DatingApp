using Api.Helpers;
using Api.Views;

namespace Api.Services;
public interface IMembersService
{
    Task<PaginatedResult<MemberView>> GetMembersAsync(PaginationFilter paginationFilter,CancellationToken cancellationToken = default!);
}