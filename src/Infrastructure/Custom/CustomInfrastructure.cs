using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure.Custom;

public class CustomUserInfrastructure : IUserAccountInfrastructure, ILogInInfrastructure
{
    private readonly UserManager<CustomUserAccountEntity> _userManager;
    private readonly SignInManager<CustomUserAccountEntity> _signInManager;

    public CustomUserInfrastructure(UserManager<CustomUserAccountEntity> userManager, SignInManager<CustomUserAccountEntity> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ApplicationResult> CreateUser(string username, string password)
    {
        CustomUserAccountEntity newUser = new()
        {
            Username = username
        };
        IdentityResult identityResult = await _userManager.CreateAsync(newUser, password);
        return identityResult.Map();
    }

    public async Task<ApplicationResult<UserAccountEntity>> ReadUserByNameAsync(string username)
    {
        CustomUserAccountEntity? identityUser = await _userManager.FindByNameAsync(username);
        
        if (identityUser == null) return ApplicationResult<UserAccountEntity>.Failure([ApplicationErrorType.invalid_credentials]);  
        UserAccountEntity user = identityUser.ToUserAccountEntity();
        return ApplicationResult<UserAccountEntity>.Success(user);
    }

    //Update

    public async Task<ApplicationResult> DeleteUserByIdAsync(string userId)
    {
        CustomUserAccountEntity? identityUser = await _userManager.FindByIdAsync(userId);
        if (identityUser == null) return ApplicationResult.Failure([ApplicationErrorType.invalid_credentials]);
        IdentityResult identityResult = await _userManager.DeleteAsync(identityUser);
        return identityResult.Map();
    }

    public async Task<ApplicationResult> CheckPasswordAsync(string username, string password)
    {
        CustomUserAccountEntity identityUser = await _userManager.FindByNameAsync(username) ?? throw new ArgumentNullException();
        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(identityUser, password, true);
        if (signInResult.Succeeded) return ApplicationResult.Success();
        if (signInResult.IsLockedOut) return ApplicationResult.Failure([ApplicationErrorType.user_locked_out]);
        if (signInResult.IsNotAllowed) return ApplicationResult.Failure([ApplicationErrorType.user_not_allowed]);
        if (signInResult.RequiresTwoFactor) return ApplicationResult.Failure([ApplicationErrorType.two_factor_required]);
        return ApplicationResult.Failure([ApplicationErrorType.invalid_credentials]);
    }
}
