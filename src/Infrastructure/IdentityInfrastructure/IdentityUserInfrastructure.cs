using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public class UserAccountInfrastructure : IUserAccountInfrastructure
{
    private readonly UserManager<IdentityUserAccountEntity> _userManager;

    public UserAccountInfrastructure(UserManager<IdentityUserAccountEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationResult> CreateUser(string username, string password)
    {
        IdentityUserAccountEntity newUser = new(username);
        IdentityResult identityResult = await _userManager.CreateAsync(newUser, password);
        return identityResult.Map();
    }

    public async Task<ApplicationResult<IUserAccountEntity>> ReadUserByNameAsync(string username)
    {
        IdentityUserAccountEntity? identityUser = await _userManager.FindByNameAsync(username);
        
        if (identityUser == null) return ApplicationResult<IUserAccountEntity>.Failure([ApplicationErrorType.invalid_credentials]);  
        IUserAccountEntity user = identityUser;
        return ApplicationResult<IUserAccountEntity>.Success(user);
    }

    //Update

    public async Task<ApplicationResult> DeleteUserAsync(IUserAccountEntity user)
    {
        IdentityUserAccountEntity identityUser = (IdentityUserAccountEntity)user;//TO DO: FIX
        IdentityResult identityResult =  await _userManager.DeleteAsync(identityUser);
        return identityResult.Map();
    }
}