using Api.Repositories.Members;

namespace Api.Services;

public class ProfileService : IProfileService
{
    private readonly IUserClaimsService _claimsService;
    private readonly IProfileRepository _profileRepository;
    public ProfileService(IUserClaimsService claimsService, IProfileRepository profileRepository)
    {
        _claimsService = claimsService;
        _profileRepository = profileRepository;
    }

    public async Task<ProfileView> GetMyProfile(CancellationToken cancellationToken = default)
    {
        string idClaim = _claimsService.ClaimsPrincipal!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        Profile? profile = await _profileRepository.ReadyByIdAsyncAsNoTracking(new Guid(idClaim), cancellationToken, true);
        return new ProfileView
        {
            FirstName = profile!.FirstName,
            LastName = profile!.LastName,
            Gender = profile!.Gender,
            Birthdate = profile!.User!.Birthdate,
            Username = profile!.User!.Username,
            Email = profile!.User!.Email,
            PhoneNumber = profile!.User!.PhoneNumber,
            ProfileId = profile!.Id,
            Biography = profile.Biography
        };
    }

    public async Task<Guid> UpdateProfile(UpdateProfileDTO updateProfileDTO, CancellationToken cancellationToken = default)
    {
        Profile? profile = await _profileRepository.ReadyByIdAsync(new Guid(_claimsService.ClaimsPrincipal!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value), cancellationToken, true);
        profile!.Update(updateProfileDTO.Bigoraphy, updateProfileDTO.FirstName, updateProfileDTO.LastName, updateProfileDTO.Gender, updateProfileDTO.Username, updateProfileDTO.Birthdate);
        await _profileRepository.UpdateAsync(profile!, cancellationToken);
        return profile.Id;
    }

}