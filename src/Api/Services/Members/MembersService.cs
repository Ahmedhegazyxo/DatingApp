using Api.Helpers;
using Api.Repositories.Members;
using Microsoft.OpenApi;

namespace Api.Services;

public class MembersService : IMembersService
{
    private readonly IUserClaimsService _userClaimsService;
    private readonly IProfileRepository _profileRepository;
    public MembersService(IUserClaimsService userClaimsService, IProfileRepository profileRepository)
    {
        _userClaimsService = userClaimsService;
        _profileRepository = profileRepository;
    }
    public async Task<List<MemberView>> GetMembersAsync(PaginationFilter? paginationFilter = null, CancellationToken cancellationToken = default!)
    {
        Guid CurrentUserId = new Guid($"{_userClaimsService.ClaimsPrincipal!.FindFirst(ClaimTypes.NameIdentifier)!.Value}");
        return await _profileRepository.ReadAsResultAsNoTracking(paginationFilter,
         e => new MemberView
         {
             Birthdate = e.User!.Birthdate,
             Id = e.Id,
             Username = e.User.Username,
             FirstName = e.FirstName,
             LastName = e.LastName,
             Gender = e.Gender,
             IsLikedBefore = e.LikesReceived.Any(e => e.CreatorId == CurrentUserId)
         }, cancellationToken, e => e.Id != CurrentUserId
        );
    }
}