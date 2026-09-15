using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Models.EntityModels;

public class UserAccountEntity : IdentityUser, IUserAccountEntity
{
    public UserAccountEntity() : base() {}
    public UserAccountEntity(string username) : base(username) {}

    public string Username
    {
        get
        {
            return base.UserName ?? throw new ArgumentNullException();
        }
        set => base.UserName = value;
    }
}

public interface IUserAccountEntity
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string? PasswordHash { get; set; }
}