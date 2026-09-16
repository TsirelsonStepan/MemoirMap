using MemoirMap.Models.EntityModels;
namespace MemoirMap.Infrastructure;

public interface IUserAccountInfrastructure
{
    public Task<ApplicationResult> CreateUser(string username, string password);

    public Task<ApplicationResult<UserAccountEntity>> ReadUserByNameAsync(string username);
    //Update
    public Task<ApplicationResult> DeleteUserByIdAsync(string userId);
}

public interface ILogInInfrastructure
{
    public Task<ApplicationResult> CheckPasswordAsync(string username, string password);
}

public interface ITokenInfrastructure
{
    public Task<string> IssueAccessTokenAsync(UserAccountEntity user);
}