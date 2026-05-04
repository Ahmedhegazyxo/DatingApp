using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Api.Services;

public class TokenService : ITokenService
{
    IConfiguration _config;
    IUserClaimsService _userClaimsService;
    private const int TokenExpirationTimeInMinutes = 15;
    public TokenService(IConfiguration config, IUserClaimsService userClaimsService)
    {
        _config = config;
        _userClaimsService = userClaimsService;
    }
    public async Task<TokenDTO> IssueToken(User user)
    {
        string tokenKeyString = _config["JsonWebTokenKey"] ?? throw new Exception("Token key was not provided");
        SymmetricSecurityKey? tokenSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKeyString));
        List<Claim> tokenClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email , user.Email),
            new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
            new Claim("Username" , user.Username),
            new Claim(ClaimTypes.Expiration , DateTime.Now.AddMinutes(TokenExpirationTimeInMinutes).ToString())
        };
        SigningCredentials tokenCredentials = new SigningCredentials(tokenSecurityKey, SecurityAlgorithms.HmacSha512Signature);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(tokenClaims),
            Expires = DateTime.Now.AddMinutes(TokenExpirationTimeInMinutes),
            SigningCredentials = tokenCredentials
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string jwtToken = await Task<string>.Run(async () =>
        {
            return tokenHandler.WriteToken(token);
        });
        return new TokenDTO
        {
            ExpiresAt = token.ValidTo,
            Token = jwtToken
        };
    }

    public bool ValidateToken(string jwtToken)
    {
        string tokenKeyString = _config["JsonWebTokenKey"] ?? throw new Exception("Token key was not provided");
        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKeyString)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;

        ClaimsPrincipal userClaims = jwtSecurityTokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
        if (securityToken.ValidTo < DateTime.UtcNow)
        {
            throw new SecurityTokenExpiredException("Session Has Expired");
        }
        _userClaimsService.SetUserClaims(userClaims);
        return true;
    }
}