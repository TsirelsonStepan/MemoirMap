namespace MemoirMap.Infrastructure.Interfaces;

public interface IAuthenticationInfrastructure
{
    public Task<ServiceResult> CreateUser(string username, string password);
}