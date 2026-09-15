namespace MemoirMap.Models.EntityModels;

public class UserAccountEntity : IUserAccountEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = null!;
    public string? PasswordHash { get; set; } = null!;
}

public interface IUserAccountEntity
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string? PasswordHash { get; set; }
}