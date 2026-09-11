using MemoirMap.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Configuration;

public static class DatabaseConfig
{
    public static IServiceCollection InitializeNpgsqlDatabase(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IServiceCollection InitializeSqliteDatabase(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        return services;
    }
}