
namespace Api.Middlewares;

public class JWTAuthenticatorMiddleware
{
    private readonly RequestDelegate _next;
    private ITokenService? _tokenService;
    public JWTAuthenticatorMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, ITokenService tokenService)
    {
        _tokenService = tokenService;
        if (context.Request.Path.ToString().StartsWith("/api", StringComparison.OrdinalIgnoreCase))
        {
            Endpoint? endpointInfo = context.GetEndpoint();
            if (endpointInfo?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }
            else
            {
                StringValues authorizationHeader = context.Request.Headers["Authorization"];
                if (StringValues.IsNullOrEmpty(authorizationHeader))
                {
                    throw new ArgumentException("No header was provided");
                }
                else
                {
                    authorizationHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase);
                    string jwtToken = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
                    _tokenService.ValidateToken(jwtToken);
                    await _next(context);
                }
            }
        }
        else
        {
            await _next(context);
        }
    }
}