namespace MemoirMap.Models.EntityModels;

public class CustomUserAccountEntity : IUserAccountEntity
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string NormalizedUsername { get; set; } = null!;
    public string? PasswordHash { get; set; }

    public CustomUserAccountEntity(string username)
    {
        Username = username;
    }
}