namespace MemoirMap.Models.EntityModels;

public class UserAccountEntity
{
    public required string Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
}