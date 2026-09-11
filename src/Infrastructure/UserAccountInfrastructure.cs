using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public class IdentityUserInfrastructure : IUserAccountInfrastructure
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityUserInfrastructure(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationResult> CreateUser(string username, string password)
    {
        IdentityUser newUser = new(username);
        IdentityResult identityResult = await _userManager.CreateAsync(newUser, password);
        return identityResult.Map();
    }

    public async Task<ApplicationResult<IdentityUser>> ReadUserByNameAsync(string username)
    {
        IdentityUser? user = await _userManager.FindByNameAsync(username);
        if (user == null) return ApplicationResult<IdentityUser>.Failure([ApplicationErrorType.invalid_credentials]);   
        return ApplicationResult<IdentityUser>.Success(user);
    }
}