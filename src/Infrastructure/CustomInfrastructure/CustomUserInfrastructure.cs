using MemoirMap.Models.EntityModels;

namespace MemoirMap.Infrastructure;

public class CustomUserInfrastructure : IUserAccountInfrastructure
{
    private readonly CustomApplicationDbContext _dbContext;

    public CustomUserInfrastructure(CustomApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationResult> CreateUser(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationResult<IUserAccountEntity>> ReadUserByNameAsync(string username)
    {
        throw new NotImplementedException();
    }

    //Update

    public async Task<ApplicationResult> DeleteUserAsync(IUserAccountEntity user)
    {
        throw new NotImplementedException();
    }
}
