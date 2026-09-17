using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure.Identity;

public class IdentityUserInfrastructure : IUserAccountInfrastructure
{
    private readonly UserManager<IdentityUserAccountEntity> _userManager;

    public IdentityUserInfrastructure(UserManager<IdentityUserAccountEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationResult> CreateUser(string username, string password)
    {
        IdentityUserAccountEntity newUser = new(username);
        IdentityResult identityResult = await _userManager.CreateAsync(newUser, password);
        return identityResult.Map();
    }

    public async Task<ApplicationResult<UserAccountEntity>> ReadUserByNameAsync(string username)
    {
        IdentityUserAccountEntity? identityUser = await _userManager.FindByNameAsync(username);
        
        if (identityUser == null) return ApplicationResult<UserAccountEntity>.Failure([ApplicationErrorType.invalid_credentials]);  
        UserAccountEntity user = identityUser.ToUserAccountEntity();
        return ApplicationResult<UserAccountEntity>.Success(user);
    }

    //Update

    public async Task<ApplicationResult> DeleteUserByIdAsync(string userId)
    {
        IdentityUserAccountEntity identityUser = await _userManager.FindByIdAsync(userId) ?? throw new ArgumentNullException();
        IdentityResult identityResult = await _userManager.DeleteAsync(identityUser);
        return identityResult.Map();
    }
}