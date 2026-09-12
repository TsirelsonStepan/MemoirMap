using MemoirMap.Infrastructure;
using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Services;

public class AccountService : IAccountService
{
    private readonly IUserAccountInfrastructure _userAccount;

    public AccountService(IUserAccountInfrastructure userAccount)
    {
        _userAccount = userAccount;
    }

    public async Task<ApplicationResult> DeleteAccount(string username)
    {
        ApplicationResult<UserAccountEntity> readUserResult = await _userAccount.ReadUserByNameAsync(username);
        if (!readUserResult.IsSuccess || readUserResult.Value == null) return ApplicationResult.Failure(readUserResult.Errors);
        return await _userAccount.DeleteUserAsync(readUserResult.Value);
    }
}