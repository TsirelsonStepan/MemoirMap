namespace MemoirMap.Models.DTOs;

public class LogInResponse
{
    public string? AccessToken { get; set; }
    public string? TokenType { get; set; }
    public int? ExpiresIn { get; set; }
    //RefreshToken
}