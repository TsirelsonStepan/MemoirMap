namespace MemoirMap.Infrastructure;

public interface IAuthenticationInfrastructure
{
    Task<ApplicationResult> CreateUser(string username, string password);
}