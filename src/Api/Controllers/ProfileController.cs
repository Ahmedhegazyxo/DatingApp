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
    public async  Task<IActionResult> GetMyProfile()
    {
        ProfileView userModel = await _profileService.GetMyProfile();
        return Ok(userModel);
    }
}