namespace Api.Services;
public interface IProfileService
{
    Task<ProfileView> GetMyProfile(CancellationToken cancellationToken = default);
    Task<ProfileView> UpdateProfile(UpdateProfileDTO updateProfileDTO, CancellationToken cancellationToken = default);
    Task<string> UpdateProfilePhoto(IFormFile formFile , CancellationToken cancellationToken = default!);
}