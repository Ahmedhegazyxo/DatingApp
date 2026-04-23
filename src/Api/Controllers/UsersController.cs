using Api.Views;

namespace Api.Controllers;
[ApiController]
[AllowAnonymous]
[Route("api/auth")]
public class UsersController : ControllerBase
{
    IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO , CancellationToken cancellationToken)
    {
        UserView userView =  await _userService.Register(registerDTO , cancellationToken);
        return Ok(userView);
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO, CancellationToken cancellationToken)
    {
        UserView userView =  await _userService.Login(loginDTO.Username, loginDTO.Password, cancellationToken);
        return Ok(userView); 
    }
}