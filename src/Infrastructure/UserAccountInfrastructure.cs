using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public class UserAccountInfrastructure : IUserAccountInfrastructure
{
    private readonly UserManager<UserAccountEntity> _userManager;

    public UserAccountInfrastructure(UserManager<UserAccountEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationResult> CreateUser(string username, string password)
    {
        UserAccountEntity newUser = new()
        {
            Username = username
        };
        IdentityResult identityResult = await _userManager.CreateAsync(newUser, password);
        return identityResult.Map();
    }

    public async Task<ApplicationResult<UserAccountEntity>> ReadUserByNameAsync(string username)
    {
        UserAccountEntity? user = await _userManager.FindByNameAsync(username);
        if (user == null) return ApplicationResult<UserAccountEntity>.Failure([ApplicationErrorType.invalid_credentials]);   
        return ApplicationResult<UserAccountEntity>.Success(user);
    }
}