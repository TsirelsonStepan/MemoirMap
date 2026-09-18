using System.Security.Cryptography;
using MemoirMap.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MemoirMap.Tests;

public abstract class TestFactoryBase : IAsyncLifetime
{
    protected IHost _host = default!;
    public IServiceProvider Services => _host.Services;
    public HttpClient CreateClient() => _host.GetTestClient();
    public IServiceScope CreateScope() => _host.Services.CreateAsyncScope();

    protected static readonly JwtOptions TestJwtOptions = new()
    {
        Issuer = "TestIssuer",
        Audience = "TestAudience",
        ExpirationMinutes = 60,
        PrivateKeyPem = NewRsaPem()
    };

    private static string NewRsaPem()
    {
        using var rsa = RSA.Create(2048);
        return rsa.ExportRSAPrivateKeyPem();
    }

    public async Task DisposeAsync()
    {
        if (_host is null) return;
        await _host.StopAsync();
        _host.Dispose();
    }

    public async Task InitializeAsync()
    {
        var builder = new HostBuilder()
            .UseEnvironment("Development")
            .ConfigureWebHost(web =>
            {
                web.UseTestServer();

                web.ConfigureServices(services =>
                {
                    System.Reflection.Assembly apiAssembly = typeof(Program).Assembly;
                    services.AddControllers().AddApplicationPart(apiAssembly);
                    services.InjectCommon();
                    services.AddExceptionHandler<ApplicationExceptionHandler>();
                    services.AddProblemDetails();
                    services.InitializeJwt(TestJwtOptions);
                    services.AddAuthorization();

                    ConfigureServices(services);
                });

                web.Configure(app =>
                {
                    app.UseDeveloperExceptionPage();
                    app.UseExceptionHandler();
                    app.UseRouting();
                    app.UseAuthentication();
                    app.UseAuthorization();
                    app.UseEndpoints(e => e.MapControllers());
                });
            });

        _host = await builder.StartAsync();
        CreateDbSchema();
    }
    public abstract void ConfigureServices(IServiceCollection services);
    public abstract void CreateDbSchema();
}