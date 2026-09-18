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
        services.InjectCustomUser();
        services.InitializeCustomIdentity();

        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        services.AddSingleton(connection);
        services.AddDbContext<CustomApplicationDbContext>(options =>
        {
            options.UseSqlite(connection);
        });
    }

    public override void CreateDbSchema()
    {
        using IServiceScope scope = _host.Services.CreateScope();
        CustomApplicationDbContext db = scope.ServiceProvider.GetRequiredService<CustomApplicationDbContext>();
        db.Database.EnsureCreated();
    }
}