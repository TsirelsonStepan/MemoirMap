using MemoirMap.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Configuration;

//TO DO: Determine the DB type by parsing connection string
//TO DO: Abstarct Factory

public static class DatabaseConfig
{
    public static IServiceCollection InitializeNpgsqlDatabaseWithIdentity(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<IdentityApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IServiceCollection InitializeSqliteDatabaseWithIdentity(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<IdentityApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        return services;
    }

    public static IServiceCollection InitializeNpgsqlDatabase(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<CustomApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IServiceCollection InitializeSqliteDatabase(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<CustomApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        return services;
    }
}