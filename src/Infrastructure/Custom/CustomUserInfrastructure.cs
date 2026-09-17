using MemoirMap.Models.EntityModels;

namespace MemoirMap.Infrastructure.Custom;

public class CustomUserInfrastructure : IUserAccountInfrastructure
{
/*
    private readonly CustomApplicationDbContext _dbContext;

    public CustomUserInfrastructure(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
*/
    public async Task<ApplicationResult> CreateUser(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationResult<UserAccountEntity>> ReadUserByNameAsync(string username)
    {
        throw new NotImplementedException();
    }

    //Update

    public async Task<ApplicationResult> DeleteUserByIdAsync(string userId)
    {
        throw new NotImplementedException();
    }
}
