namespace Api.Services;

public class UserClaimsService : IUserClaimsService
{
    public ClaimsPrincipal? ClaimsPrincipal {get; private set;}

    public void SetUserClaims(ClaimsPrincipal userClaims)
    {
        ClaimsPrincipal = userClaims;
    }
}