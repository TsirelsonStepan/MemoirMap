using MemoirMap.Services.Interfaces;
using MemoirMap.Infrastructure.Interfaces;

namespace MemoirMap.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationInfrastructure _infrastructure;

    public AuthenticationService(IAuthenticationInfrastructure infrastructure)
    {
        _infrastructure = infrastructure;
    }

    public Task<ServiceResult<string>> SignIn(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> Register(string username, string password)
    {
        return await _infrastructure.CreateUser(username, password);
    }
}