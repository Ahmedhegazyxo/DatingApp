using Microsoft.AspNetCore.SignalR;

namespace Api.Services;

public class HubConnectionIdProvider : IUserIdProvider
{
    IConfiguration _config;

    public HubConnectionIdProvider(IConfiguration config)
    {
        _config = config;
    }
    public string? GetUserId(HubConnectionContext connection)
    {
        var httpContext = connection.GetHttpContext();
        if (httpContext == null)
        {
            throw new ArgumentNullException("Can't Produce HTTP Context from SignalR Connection");
        }
        string jwtToken = httpContext.Request.Query["access_token"].ToString();
        if (string.IsNullOrEmpty(jwtToken))
            return null;
        string? key = _config["JsonWebTokenKey"];
        ClaimsPrincipal claims = UserClaimsProvider.GetClaimsFromToken(jwtToken, key!, out _);
        string? userId =  claims.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
}