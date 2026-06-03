using System.Data;
using Api.Repositories;
using Api.Repositories.Members;
using Api.Views;
using Microsoft.EntityFrameworkCore.Storage;
using SQLitePCL;

namespace Api.Services;

public class MemberMatchingService : IMemberMatchingService
{
    private readonly IUserClaimsService _userClaimsService;
    private readonly ApplicationDbContext _context;
    private readonly IProfileRepository _profileRepository;
    public MemberMatchingService(IUserClaimsService userClaimsService,
    IProfileRepository profileRepository,
     ApplicationDbContext context)
    {
        _profileRepository = profileRepository;
        _userClaimsService = userClaimsService;
        _context = context;
    }

    public async Task<LikeResponseView> LikeAndPossibleMatch(Guid receptorId, CancellationToken cancellationToken = default!)
    {
        Guid currentUserId = Guid.Parse(
            _userClaimsService.ClaimsPrincipal!
                .FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        bool isMatch = false;
        bool isLikedBefore = await _profileRepository.ExistsAsync(e => e.LikesSent.Any(e => e.ReceptorId == receptorId) && e.Id == currentUserId, cancellationToken);
        if (isLikedBefore)
            throw new DuplicateNameException("Member already was liked before");
        else
        {
            Profile? profile = await _profileRepository.ReadyByIdAsync(currentUserId, cancellationToken, true);
            if (profile!.LikesReceived.Any(e => e.CreatorId == receptorId))
            {
                profile.AddMatch(currentUserId, receptorId);
                isMatch = true;
            }
            profile.AddLike(currentUserId, receptorId);
            await _profileRepository.UpdateAsync(profile, cancellationToken);
            return new LikeResponseView { IsMatched = isMatch, IsLiked = true, UserId = receptorId };
        }
    }
}