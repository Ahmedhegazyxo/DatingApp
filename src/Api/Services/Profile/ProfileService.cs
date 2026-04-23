namespace Api.Services;

public class ProfileService : IProfileService
{
    private readonly IUserClaimsService _claimsService;
    private readonly ApplicationDbContext _context;
    public ProfileService(IUserClaimsService claimsService, ApplicationDbContext context)
    {
        _claimsService = claimsService;
        _context = context;
    }

    public async Task<ProfileView> GetMyProfile()
    {
        string idClaim = _claimsService.ClaimsPrincipal!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return await _context.Set<User>().Where(u => u.Id == new Guid(idClaim)).Select(e => new ProfileView
        {
            FirstName = e.Profile!.FirstName,
            LastName = e.Profile.LastName,
            PhoneNumber = e.PhoneNumber,
            Birthdate = e.Birthdate,
            Email = e.Email,
            Gender = e.Profile.Gender,
            ProfileId = e.Id,
            Username = e.Username
        }).AsNoTracking().FirstAsync();
    }
}