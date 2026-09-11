using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public interface IUserAccountInfrastructure
{
    public Task<ApplicationResult> CreateUser(string username, string password);

    public Task<ApplicationResult<IdentityUser>> ReadUserByNameAsync(string username);
}

public interface ILogInInfrastructure
{
    public Task<ApplicationResult> CheckPasswordAsync(IdentityUser user, string password);
}

public interface ITokenInfrastructure
{
    public Task<string> IssueAccessTokenAsync(IdentityUser user);
}