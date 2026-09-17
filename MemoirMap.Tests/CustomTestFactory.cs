using Microsoft.Extensions.DependencyInjection;

using MemoirMap.Configuration;
using MemoirMap.Infrastructure.Custom;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Tests;

public sealed class CustomTestFactory : TestFactoryBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        System.Reflection.Assembly apiAssembly = typeof(Program).Assembly;
        services.AddControllers().AddApplicationPart(apiAssembly);

        services.InjectServices();

        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        services.AddSingleton(connection);
        services.AddDbContext<CustomApplicationDbContext>(options =>
        {
            options.UseSqlite(connection);
        });

        services.InitializeCustomIdentity();
        services.AddExceptionHandler<ApplicationExceptionHandler>();
        services.AddProblemDetails();
        services.InitializeJwt(TestJwtOptions, TestPrivateKeyPem);
        services.AddAuthorization();
    }

    public override void CreateDbSchema()
    {
        using IServiceScope scope = _host.Services.CreateScope();
        CustomApplicationDbContext db = scope.ServiceProvider.GetRequiredService<CustomApplicationDbContext>();
        db.Database.EnsureCreated();
    }
}