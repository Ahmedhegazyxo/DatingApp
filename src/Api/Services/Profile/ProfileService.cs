using Api.Helpers;
using Api.Repositories.Members;
using Microsoft.Extensions.Options;

namespace Api.Services;

public class ProfileService : IProfileService
{
    private readonly IUserClaimsService _claimsService;
    private readonly IProfileRepository _profileRepository;
    private readonly IOptions<FileStorageConfiguration> _fileStorageConfigs;
    private readonly IFileService _fileService;
    public ProfileService(IUserClaimsService claimsService,
     IProfileRepository profileRepository,
     IOptions<FileStorageConfiguration> fileStorageConfig,
     IFileService fileService)
    {
        _claimsService = claimsService;
        _profileRepository = profileRepository;
        _fileStorageConfigs = fileStorageConfig;
        _fileService = fileService;
    }

    public async Task<ProfileView> GetMyProfile(CancellationToken cancellationToken = default)
    {
        string idClaim = _claimsService.ClaimsPrincipal!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        Profile? profile = await _profileRepository.ReadyByIdAsyncAsNoTracking(new Guid(idClaim), cancellationToken, true);
        return new ProfileView
        {
            FirstName = profile!.FirstName,
            LastName = profile.LastName,
            Gender = profile.Gender,
            Birthdate = profile.User!.Birthdate,
            Username = profile.User.Username,
            Email = profile.User.Email,
            PhoneNumber = profile.User.PhoneNumber,
            ProfileId = profile.Id,
            Biography = profile.Biography,
            ProfilePhotoId = profile?.ProfilePhoto?.AttachmentId.ToString()
        };
    }


    public async Task<ProfileView> UpdateProfile(UpdateProfileDTO updateProfileDTO, CancellationToken cancellationToken = default)
    {
        Profile? profile = await _profileRepository.ReadyByIdAsync(new Guid(_claimsService.ClaimsPrincipal!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value), cancellationToken, true);
        profile!.Update(updateProfileDTO.Biography, updateProfileDTO.FirstName, updateProfileDTO.LastName, updateProfileDTO.Gender, updateProfileDTO.Username, updateProfileDTO.Birthdate);
        profile = await _profileRepository.UpdateAsync(profile!, cancellationToken);
        return new ProfileView
        {
            Username = profile.User!.Username,
            Email = profile.User.Email,
            Biography = profile.Biography,
            Birthdate = profile.User.Birthdate,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Gender = profile.Gender,
            PhoneNumber = profile.User.PhoneNumber,
            ProfileId = profile.Id,
            ProfilePhotoId = profile.ProfilePhoto?.AttachmentId.ToString()
        };
    }

    public async Task<string> UpdateProfilePhoto(IFormFile formFile, CancellationToken cancellationToken)
    {
        string idClaim = _claimsService.ClaimsPrincipal!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        Profile? profile = await _profileRepository.ReadyByIdAsync(new Guid(idClaim), cancellationToken, true);
        FileMetaData fileMetaData = new FileMetaData
        {
            FileName = profile?.ProfilePhoto == null ? Guid.NewGuid().ToString() : profile.ProfilePhoto.Attachment!.FileName, 
            OpenStream = () => formFile.OpenReadStream(),
            FileExtension = "." + formFile.FileName.Split('.').Last(),
            FileRootPath = Path.Combine(DateTime.UtcNow.Year.ToString(), DateTime.UtcNow.Month.ToString(), DateTime.UtcNow.Day.ToString())
        };
        bool isFileCreated = await _fileService.CreateFile(fileMetaData);
        profile!.AddProfilePhoto(fileMetaData.FileName, fileMetaData.FileRootPath, fileMetaData.FileExtension);
        profile = await _profileRepository.UpdateAsync(profile, cancellationToken);
        return profile.ProfilePhoto!.Id.ToString();
    }
}