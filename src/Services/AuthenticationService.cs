using MemoirMap.Infrastructure;

namespace MemoirMap.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationInfrastructure _infrastructure;

    public AuthenticationService(IAuthenticationInfrastructure infrastructure)
    {
        _infrastructure = infrastructure;
    }

    public Task<ApplicationResult<string>> SignIn(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationResult> Register(string username, string password)
    {
        return await _infrastructure.CreateUser(username, password);
    }
}