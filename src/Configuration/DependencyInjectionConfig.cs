using MemoirMap.Infrastructure;
using MemoirMap.Services;

namespace MemoirMap.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
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

        services.AddScoped<ILogInInfrastructure, LogInInfrastructure>();
        services.AddScoped<IUserAccountInfrastructure, IdentityUserInfrastructure>();
        services.AddScoped<ITokenInfrastructure, JwtTokenInfrastructure>();

        return services;
    }
}