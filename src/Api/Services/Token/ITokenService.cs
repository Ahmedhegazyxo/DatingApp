namespace Api.Services;
public interface ITokenService
{
    string IssueToken(User user);
    bool ValidateToken(string jwtToken);
}