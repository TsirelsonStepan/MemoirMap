using MemoirMap.Models.EntityModels;

namespace MemoirMap.Infrastructure;

public interface IUserAccountInfrastructure
{
    public Task<ApplicationResult> CreateUser(string username, string password);

    public Task<ApplicationResult<IUserAccountEntity>> ReadUserByNameAsync(string username);
    //Update
    public Task<ApplicationResult> DeleteUserAsync(IUserAccountEntity user);
}

public interface ILogInInfrastructure
{
    public Task<ApplicationResult> CheckPasswordAsync(IUserAccountEntity user, string password);
}

public interface ITokenInfrastructure
{
    public Task<string> IssueAccessTokenAsync(IUserAccountEntity user);
}