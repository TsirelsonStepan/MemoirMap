using Microsoft.Extensions.DependencyInjection;

using MemoirMap.Configuration;
using Microsoft.Data.Sqlite;
using MemoirMap.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Tests;

public sealed class IdentityTestFactory : TestFactoryBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        System.Reflection.Assembly apiAssembly = typeof(Program).Assembly;
        services.AddControllers().AddApplicationPart(apiAssembly);

        services.InjectServices();

        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        services.AddSingleton(connection);
        services.AddDbContext<IdentityApplicationDbContext>(options =>
        {
            options.UseSqlite(connection);
        });
        
        services.InitializeIdentity();
        services.AddExceptionHandler<ApplicationExceptionHandler>();
        services.AddProblemDetails();
        services.InitializeJwt(TestJwtOptions, TestPrivateKeyPem);
        services.AddAuthorization();
    }

    public override void CreateDbSchema()
    {
        using IServiceScope scope = _host.Services.CreateScope();
        IdentityApplicationDbContext db = scope.ServiceProvider.GetRequiredService<IdentityApplicationDbContext>();
        db.Database.EnsureCreated();
    }
}