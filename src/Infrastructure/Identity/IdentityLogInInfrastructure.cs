using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure.Identity;

public class IdentityLogInInfrastructure : ILogInInfrastructure
{
    private readonly SignInManager<IdentityUserAccountEntity> _signInManager;

    public IdentityLogInInfrastructure(SignInManager<IdentityUserAccountEntity> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<ApplicationResult> CheckPasswordAsync(string username, string password)
    {
        IdentityUserAccountEntity identityUser = await _signInManager.UserManager.FindByNameAsync(username) ?? throw new ArgumentNullException();
        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(identityUser, password, true);
        if (signInResult.Succeeded) return ApplicationResult.Success();
        if (signInResult.IsLockedOut) return ApplicationResult.Failure([ApplicationErrorType.user_locked_out]);
        if (signInResult.IsNotAllowed) return ApplicationResult.Failure([ApplicationErrorType.user_not_allowed]);
        if (signInResult.RequiresTwoFactor) return ApplicationResult.Failure([ApplicationErrorType.two_factor_required]);
        return ApplicationResult.Failure([ApplicationErrorType.invalid_credentials]);
    }
}