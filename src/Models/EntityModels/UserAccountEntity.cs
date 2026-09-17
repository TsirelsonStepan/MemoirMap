namespace MemoirMap.Models.EntityModels;

public class UserAccountEntity
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string? PasswordHash { get; set; }
}