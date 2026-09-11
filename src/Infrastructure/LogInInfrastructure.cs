using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public class LogInInfrastructure : ILogInInfrastructure
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public LogInInfrastructure(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<ApplicationResult> CheckPasswordAsync(IdentityUser user, string password)
    {
        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, true);
        if (signInResult.Succeeded) return ApplicationResult.Success();
        if (signInResult.IsLockedOut) return ApplicationResult.Failure([ApplicationErrorType.user_locked_out]);
        if (signInResult.IsNotAllowed) return ApplicationResult.Failure([ApplicationErrorType.user_not_allowed]);
        if (signInResult.RequiresTwoFactor) return ApplicationResult.Failure([ApplicationErrorType.two_factor_required]);
        return ApplicationResult.Failure([ApplicationErrorType.invalid_credentials]);
    }
}