using MemoirMap.Infrastructure;

namespace MemoirMap.Services;

public class AccountService : IAccountService
{
    private readonly IUserAccountInfrastructure _userAccount;

    public AccountService(IUserAccountInfrastructure userAccount)
    {
        _userAccount = userAccount;
    }

    public async Task<ApplicationResult> DeleteAccount(string userId)
    {
        return await _userAccount.DeleteUserByIdAsync(userId);
    }
}