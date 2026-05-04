namespace Api.Services;
public interface IProfileService
{
    Task<ProfileView> GetMyProfile(CancellationToken cancellationToken = default);
    Task<Guid> UpdateProfile(UpdateProfileDTO updateProfileDTO, CancellationToken cancellationToken = default);
}