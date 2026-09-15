namespace MemoirMap.Models.EntityModels;

public class UserAccountEntity : IUserAccountEntity
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

public interface IUserAccountEntity
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string NormalizedUsername { get; set; }
    public string? PasswordHash { get; set; }
}