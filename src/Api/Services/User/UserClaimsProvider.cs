namespace Api.Services;

public static class UserClaimsProvider
{
    public static ClaimsPrincipal GetClaimsFromToken(string jwtToken, string tokenKeyString, out SecurityToken securityToken)
    {
        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKeyString)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal userClaims = jwtSecurityTokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
        return userClaims;
    }
}