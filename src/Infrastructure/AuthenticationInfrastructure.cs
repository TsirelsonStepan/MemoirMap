using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public class AuthenticationInfrastructure: IAuthenticationInfrastructure
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationInfrastructure(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationResult> CreateUser(string username, string password)
    {
        IdentityUser newUser = new(username);
        IdentityResult identityResult = await _userManager.CreateAsync(newUser, password);
        return identityResult.Map();
    }
}