using MemoirMap.Infrastructure;
using MemoirMap.Infrastructure.Interfaces;
using MemoirMap.Services;
using MemoirMap.Services.Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IEditExperienceService, EditExperienceService>();
        services.AddScoped<IGetExperienceService, GetExperienceService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ISaveLocationService, SaveLocationService>();
        services.AddScoped<ISaveExperienceService, SaveExperienceService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        services.AddScoped<IAuthenticationInfrastructure, AuthenticationInfrastructure>();

        return services;
    }
}