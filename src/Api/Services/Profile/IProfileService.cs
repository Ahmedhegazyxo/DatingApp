namespace Api.Services;
public interface IProfileService
{
    Task<ProfileView> GetMyProfile();
}