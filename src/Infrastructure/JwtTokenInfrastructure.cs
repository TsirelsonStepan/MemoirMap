using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

using MemoirMap.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace MemoirMap.Infrastructure;

public class JwtTokenInfrastructure : ITokenInfrastructure
{
    private readonly JwtOptions _jwtOptions;
    private readonly IApplicationSigningKey _signingKey;
    private readonly JwtSecurityTokenHandler _jwtHandler = new();

    public JwtTokenInfrastructure(IOptions<JwtOptions> options, IApplicationSigningKey signingKey)
    {
        _jwtOptions = options.Value;
        _signingKey = signingKey;
    }

    private async Task<string> CreateAccessTokenAsync(IdentityUser user)
    {
        string userId = user.Id;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
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

    public async Task<string> IssueAccessTokenAsync(IdentityUser user)
    {
        string accessToken = await CreateAccessTokenAsync(user);

        return accessToken;
    }
}