using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Models.EntityModels;

public class UserAccountEntity : IdentityUser
{
    public UserAccountEntity() : base() {}
    public UserAccountEntity(string username) : base(username) {}
}