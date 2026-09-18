using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using MemoirMap.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using MemoirMap.Models.EntityModels;

namespace MemoirMap.Infrastructure;

public class JwtInfrastructure : ITokenInfrastructure
{
    private readonly JwtOptions _jwtOptions;
    private readonly RsaSigningKey _signingKey;
    private readonly JwtSecurityTokenHandler _jwtHandler = new();

    public JwtInfrastructure(IOptions<JwtOptions> options, RsaSigningKey signingKey)
    {
        _jwtOptions = options.Value;
        _signingKey = signingKey;
    }

    private async Task<string> CreateAccessTokenAsync(UserAccountEntity user)
    {
        string userId = user.Id;

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
        };

        var token = new JwtSecurityToken
        (
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
            signingCredentials: new SigningCredentials(_signingKey.GetSigningKey(), _signingKey.GetAlgorithm())
        );

        return _jwtHandler.WriteToken(token);
    }

    public async Task<string> IssueAccessTokenAsync(UserAccountEntity user)
    {
        string accessToken = await CreateAccessTokenAsync(user);

        return accessToken;
    }
}