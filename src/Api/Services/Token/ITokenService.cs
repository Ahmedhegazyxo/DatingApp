namespace Api.Services;
public interface ITokenService
{
    Task<TokenDTO> IssueToken(User user);
    bool ValidateToken(string jwtToken);
}