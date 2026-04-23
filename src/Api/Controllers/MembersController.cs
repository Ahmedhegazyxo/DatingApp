using Api.Helpers;
namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMembersService _membersService;
    private readonly IMemberMatchingService _matchingService;
    public MembersController(IMembersService membersService, IMemberMatchingService matchingService)
    {
        _membersService = membersService;
        _matchingService = matchingService;
    }
    [HttpGet]
    public async Task<List<MemberView>> GetMembers([FromQuery] PaginationFilter? paginationFilter)
    {
        return await _membersService.GetMembersAsync();
    }
    [HttpPost("likeOrMatch/{id}")]
    public async Task<IActionResult> LikeOrMatch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _matchingService.LikeAndPossibleMatch(id , cancellationToken));
    }
}