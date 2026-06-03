
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
        User? user = await _context.Set<User>().FirstOrDefaultAsync(e => e.Username == userName);
        if (user == null)
            throw new InvalidCredentialException("User not found");

        bool isCredentialsCorrect = PasswordHasher.VerifyPassword(password, user.HashPassword, user.HashSalt);
        if (isCredentialsCorrect == false)
            throw new InvalidCredentialException("Incorrect password");
        else
        {

            TokenDTO tokenDTO = await _tokenService.IssueToken(user);
            return new UserView
            {
                Id = user.Id,
                FirstName = user.Profile!.FirstName,
                LastName = user.Profile!.LastName,
                Username = user.Username,
                ExpiresAt = tokenDTO.ExpiresAt,
                Token = tokenDTO.Token
            };
        }
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
        TokenDTO token = await _tokenService.IssueToken(createdUser.Entity);
        return new UserView
        {
            Username = createdUser.Entity.Username,
            FirstName = createdUser.Entity.Profile!.FirstName,
            LastName = createdUser.Entity.Profile!.LastName,
            Token = token.Token,
            ExpiresAt = token.ExpiresAt
        };
    }
}