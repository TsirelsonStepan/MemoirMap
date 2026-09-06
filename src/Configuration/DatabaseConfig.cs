using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Configuration;

public static class DatabaseConfig
{
    public static IServiceCollection InitializeNpgsqlDatabase(this IServiceCollection services, string? connectionString)
    {
        if (connectionString == null) throw new Exception();
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}