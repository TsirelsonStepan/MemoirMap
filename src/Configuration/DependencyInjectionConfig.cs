using MemoirMap.Infrastructure;
using MemoirMap.Infrastructure.Custom;
using MemoirMap.Infrastructure.Identity;
using MemoirMap.Services;

namespace MemoirMap.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection InjectCommon(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ILogInService, LogInService>();
        services.AddScoped<ISignUpService, SignUpService>();
        services.AddScoped<IEditExperienceService, EditExperienceService>();
        services.AddScoped<IGetExperienceService, GetExperienceService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ISaveLocationService, SaveLocationService>();
        services.AddScoped<ISaveExperienceService, SaveExperienceService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        services.AddScoped<ITokenInfrastructure, JwtInfrastructure>();

        return services;
    }

    public static IServiceCollection InjectCustomUser(this IServiceCollection services)
    {
        services.AddScoped<ILogInInfrastructure, CustomUserInfrastructure>();
        services.AddScoped<IUserAccountInfrastructure, CustomUserInfrastructure>();

        return services;
    }

    public static IServiceCollection InjectIdentityUser(this IServiceCollection services)
    {
        services.AddScoped<ILogInInfrastructure, IdentityLogInInfrastructure>();
        services.AddScoped<IUserAccountInfrastructure, IdentityUserInfrastructure>();

        return services;
    }
}