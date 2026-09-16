using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Models.EntityModels;

public class IdentityUserAccountEntity : IdentityUser, IUserAccountEntity
{
    public IdentityUserAccountEntity() : base() {}
    public IdentityUserAccountEntity(string username) : base(username) {}

    public string Username { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string NormalizedUsername { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}