using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Models.EntityModels;

public class IdentityUserAccountEntity : IdentityUser
{
    public IdentityUserAccountEntity() : base() {}
    public IdentityUserAccountEntity(string username) : base(username) {}
}

public static class IdentityUserAccountEntityMapper
{
    public static IdentityUserAccountEntity ToIdentityUserAccountEntity(this UserAccountEntity user)
    {
        return new IdentityUserAccountEntity()
        {
            Id = user.Id,
            UserName = user.Username,
            PasswordHash = user.PasswordHash
        };
    }

    public static UserAccountEntity ToUserAccountEntity(this IdentityUserAccountEntity user)
    {
        return new UserAccountEntity()
        {
            Id = user.Id,
            Username = user.UserName ?? throw new ArgumentNullException(),
            PasswordHash = user.PasswordHash ?? throw new ArgumentNullException()
        };
    }
}