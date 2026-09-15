namespace MemoirMap.Models.EntityModels;

public class CustomUserAccountEntity : UserAccountEntity
{
    public CustomUserAccountEntity(string username) : base(username) { }
}

public abstract class UserAccountEntity
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string NormalizedUsername { get; set; } = null!;
    public string? PasswordHash { get; set; }

    public UserAccountEntity(string username)
    {
        Username = username;
    }
}