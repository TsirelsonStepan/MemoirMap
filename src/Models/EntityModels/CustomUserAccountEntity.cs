namespace MemoirMap.Models.EntityModels;

public class CustomUserAccountEntity
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string NormalizedUsername { get; set; } = null!;
    public string? PasswordHash { get; set; }
}

public static class CustomUserAccountEntityMapper
{
    public static UserAccountEntity ToUserAccountEntity(this CustomUserAccountEntity user)
    {
        return new UserAccountEntity()
        {
            Id = user.Id,
            Username = user.Username,
            PasswordHash = user.PasswordHash ?? throw new ArgumentNullException()
        };
    }

    public static CustomUserAccountEntity FromUserAccountEntity(this UserAccountEntity user)
    {
        return new CustomUserAccountEntity()
        {
            Id = user.Id,
            Username = user.Username,
            PasswordHash = user.PasswordHash
        };
    }
}