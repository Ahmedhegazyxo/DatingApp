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

    public async Task<PaginatedResult<MemberView>> GetMatchesAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken = default)
    {
        Guid CurrentUserId = new Guid($"{_userClaimsService.ClaimsPrincipal!.FindFirst(ClaimTypes.NameIdentifier)!.Value}");
        return await _profileRepository.ReadAsResult(
            paginationFilter,
         e => new MemberView
         {
             Birthdate = e.User!.Birthdate,
             Id = e.Id,
             Username = e.User.Username,
             FirstName = e.FirstName,
             LastName = e.LastName,
             Gender = e.Gender,
             IsLikedBefore = true
         }, cancellationToken, e => e.MatchesSent.Any(e => e.CreatorProfileId == CurrentUserId) || e.MatchesReceived.Any(e => e.ReceptorProfileId == CurrentUserId)
        );
    }

    public async Task<PaginatedResult<MemberView>> GetMembersAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken = default!)
    {
        Guid CurrentUserId = new Guid($"{_userClaimsService.ClaimsPrincipal!.FindFirst(ClaimTypes.NameIdentifier)!.Value}");
        return await _profileRepository.ReadAsResult(paginationFilter,
         e => new MemberView
         {
             Birthdate = e.User!.Birthdate,
             Id = e.Id,
             Username = e.User.Username,
             FirstName = e.FirstName,
             LastName = e.LastName,
             Gender = e.Gender,
             ProfilePhotoId = e.ProfilePhoto == null ? null : e.ProfilePhoto!.AttachmentId.ToString(),
             IsLikedBefore = e.LikesReceived.Any(e => e.CreatorId == CurrentUserId)
         }, cancellationToken, e => e.Id != CurrentUserId
        );
    }
}