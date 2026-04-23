using Microsoft.AspNetCore.Identity;

namespace Api.Services;

public interface IUserClaimsService
{
    ClaimsPrincipal? ClaimsPrincipal {get;}
    void SetUserClaims(ClaimsPrincipal userClaims);
}