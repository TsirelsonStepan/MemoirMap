using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MemoirMap.Configuration;

public static class JwtConfig
{
    public static IServiceCollection InitializeJwt(this IServiceCollection services, JwtOptions jwtOptions, IApplicationSigningKey securityKey)
    {
        if (jwtOptions.Issuer == null || jwtOptions.Audience == null)
            throw new InvalidOperationException("JWT configuration is invalid.");

        services.Configure<JwtOptions>(o =>
        {
            o.Issuer = jwtOptions.Issuer;
            o.Audience = jwtOptions.Audience;
            o.ExpirationMinutes = jwtOptions.ExpirationMinutes;
        });

        services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,

                ValidateLifetime = true,

                ValidAlgorithms = [securityKey.GetAlgorithm()],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey.GetPublicKey(),
            };
        });

        services.AddSingleton(securityKey);

        return services;
    }
}

public interface IApplicationSigningKey
{
    public SecurityKey GetSigningKey();
    public SecurityKey GetPublicKey();
    public string GetAlgorithm();
}

public class RsaSigningKey : IApplicationSigningKey
{
    private readonly RsaSecurityKey _privateKey;
    private readonly RsaSecurityKey _publicKey;

    public RsaSigningKey(string privateKeyPem)
    {
        RSA rsa = RSA.Create();
        rsa.ImportFromPem(privateKeyPem);
        _privateKey = new RsaSecurityKey(rsa);

        RSA publicRsa = RSA.Create();
        publicRsa.ImportSubjectPublicKeyInfo(rsa.ExportSubjectPublicKeyInfo(), out _);
        _publicKey = new RsaSecurityKey(publicRsa);
    }

    public SecurityKey GetSigningKey() => _privateKey;
    public SecurityKey GetPublicKey() => _publicKey;

    public string GetAlgorithm() => SecurityAlgorithms.RsaSha256;
}

public class JwtOptions
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int ExpirationMinutes { get; set; }
}