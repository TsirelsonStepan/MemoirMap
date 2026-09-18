using MemoirMap.Infrastructure.Custom;
using MemoirMap.Infrastructure.Identity;
using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection InitializeIdentity(this IServiceCollection services)
    {
        services
        .AddIdentityCore<IdentityUserAccountEntity>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<IdentityApplicationDbContext>()
        .AddSignInManager();

        return services;
    }

    public static IServiceCollection InitializeCustomIdentity(this IServiceCollection services)
    {
        services
        .AddIdentityCore<CustomUserAccountEntity>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddUserStore<CustomUserStore>()
        .AddSignInManager();

        return services;
    }
}