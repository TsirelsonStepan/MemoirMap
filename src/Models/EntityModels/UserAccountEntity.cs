namespace MemoirMap.Models.EntityModels;

public interface IUserAccountEntity
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string NormalizedUsername { get; set; }
    public string? PasswordHash { get; set; }
}