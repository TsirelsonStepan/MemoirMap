
using MemoirMap.Infrastructure;
using MemoirMap.Models.EntityModels;

namespace MemoirMap.Services;

public class LogInService : ILogInService
{
    private readonly IUserAccountInfrastructure _userAccount;
    private readonly ITokenInfrastructure _token;
    private readonly ILogInInfrastructure _logIn;

    public LogInService(IUserAccountInfrastructure userAccount, ITokenInfrastructure token, ILogInInfrastructure logIn)
    {
        _userAccount = userAccount;
        _logIn = logIn;
        _token = token;
    }

    public async Task<ApplicationResult<string>> LogIn(string username, string password)
    {
        ApplicationResult<UserAccountEntity> readUserResult = await _userAccount.ReadUserByNameAsync(username);
        if (!readUserResult.IsSuccess || readUserResult.Value == null) return ApplicationResult<string>.Failure(readUserResult.Errors);
        
        ApplicationResult checkPasswordResult = await _logIn.CheckPasswordAsync(readUserResult.Value, password);
        if (!checkPasswordResult.IsSuccess) return ApplicationResult<string>.Failure(checkPasswordResult.Errors);

        string accessToken = await _token.IssueAccessTokenAsync(readUserResult.Value);
        
        return ApplicationResult<string>.Success(accessToken);
    }
}

public class SignUpService : ISignUpService
{
    private readonly IUserAccountInfrastructure _userAccount;

    public SignUpService(IUserAccountInfrastructure userAccount)
    {
        _userAccount = userAccount;
    }

    public async Task<ApplicationResult> SignUp(string username, string password)
    {
        return await _userAccount.CreateUser(username, password);
    }
}