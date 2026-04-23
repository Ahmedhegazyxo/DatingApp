
using System.Security.Authentication;
using Api.Views;

namespace Api.Services;

public class UserService : IUserService
{
    private ApplicationDbContext _context;
    private ITokenService _tokenService;
    public UserService(ApplicationDbContext context, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _context = context;
    }
    public Task<Guid> Deactivate(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserView> Login(string userName, string password, CancellationToken cancellationToken)
    {
        User user = await _context.Set<User>().Include(e=>e.Profile).FirstAsync(e => e.Username == userName);
        bool isCredentialsCorrect = PasswordHasher.VerifyPassword(password, user.HashPassword, user.HashSalt);
        if (isCredentialsCorrect == false)
            throw new InvalidCredentialException("Incorrect username or password");
        else
            return new UserView
            {
                Token = _tokenService.IssueToken(user),
                FirstName = user.Profile!.FirstName,
                LastName = user.Profile!.LastName,
                Username = user.Username
            };
    }

    public async Task<UserView> Register(RegisterDTO registerDto, CancellationToken cancellationToken)
    {
        (string Hash, string Salt) passwordCollection = PasswordHasher.HashPassword(registerDto.Password);
        User user = User.Create(registerDto.Username,
         registerDto.Email,
          registerDto.Birthdate,
           passwordCollection.Hash,
            passwordCollection.Salt,
            registerDto.FirstName,
            registerDto.LastName,
            registerDto.PhoneNumber,
             registerDto.Gender);
        EntityEntry<User> createdUser = await _context.Set<User>().AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync();
        return new UserView
        {
           Token = _tokenService.IssueToken(createdUser.Entity),
           Username = createdUser.Entity.Username,  
           FirstName = createdUser.Entity.Profile!.FirstName,
           LastName = createdUser.Entity.Profile!.LastName
        };
    }
}