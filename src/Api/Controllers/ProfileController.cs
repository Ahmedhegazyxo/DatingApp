namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }
    [HttpGet]
    public async Task<IActionResult> GetMyProfile(CancellationToken cancellationToken)
    {
        ProfileView userModel = await _profileService.GetMyProfile(cancellationToken);
        return Ok(userModel);
    }
    [HttpGet("metrics")]
    public async Task<IActionResult> GetProfileMetrics(CancellationToken cancellationToken)
    {
        return Ok(await _profileService.GetProfileMetrcis(cancellationToken));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDTO, CancellationToken cancellationToken)
    {
        ProfileView profileView = await _profileService.UpdateProfile(updateProfileDTO, cancellationToken);
        return Ok(profileView);
    }
    [HttpPut("photo")]
    public async Task<IActionResult> UpdateProfilePhoto(IFormFile formFile, CancellationToken cancellationToken)
    {
        string profilePhotoId = await _profileService.UpdateProfilePhoto(formFile, cancellationToken);
        return Ok(new
        {
            ProfilePhotoId = profilePhotoId
        });
    }
}