using Api.Helpers;
using Api.Repositories;
using Api.Views;

namespace Api.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IUserClaimsService _userClaimsService;

    public MatchService(IMatchRepository matchRepository, IUserClaimsService userClaimsService)
    {
        _matchRepository = matchRepository;
        _userClaimsService = userClaimsService;
    }

    public async Task<PaginatedResult<MemberView>> GetMatchesAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken = default)
    {
        Guid userId = Guid.Parse(_userClaimsService.ClaimsPrincipal!.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return await _matchRepository.ReadAsResult(paginationFilter, e => new MemberView
        {
            FirstName = e.ReceptorProfileId == userId ? e.CreatorProfile!.FirstName : e.ReceptorProfile!.FirstName,
            Gender = e.ReceptorProfileId == userId ? e.CreatorProfile!.Gender : e.ReceptorProfile!.Gender,
            LastName = e.ReceptorProfileId == userId ? e.CreatorProfile!.LastName : e.ReceptorProfile!.LastName,
            Id = e.ReceptorProfileId == userId ? e.CreatorProfileId : e.ReceptorProfileId,
            Username = e.ReceptorProfileId == userId ? e.CreatorProfile!.User!.Username : e.ReceptorProfile!.User!.Username,
            Birthdate = e.ReceptorProfileId == userId ? e.CreatorProfile!.User!.Birthdate : e.ReceptorProfile!.User!.Birthdate,
            ProfilePhotoId = e.ReceptorProfileId == userId ?
            (e.CreatorProfile!.ProfilePhoto == null ? null : e.CreatorProfile.ProfilePhoto.AttachmentId.ToString()) :
            (e.ReceptorProfile!.ProfilePhoto == null ? null : e.ReceptorProfile.ProfilePhoto.AttachmentId.ToString()),
            MatchId = e.Id,
            IsLiked = true,
            IsMatched = true
        }, cancellationToken, e => e.CreatorProfileId == userId || e.ReceptorProfileId == userId);
    }
}