namespace Api.Controllers;
[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }
    [HttpGet]
    public async  Task<IActionResult> GetMyProfile(CancellationToken cancellationToken)
    {
        ProfileView userModel = await _profileService.GetMyProfile(cancellationToken);
        return Ok(userModel);
    }
    [HttpPut]
    public async  Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDTO, CancellationToken cancellationToken)
    {
        Guid id = await _profileService.UpdateProfile(updateProfileDTO, cancellationToken);
        return Ok(id);
    }
}