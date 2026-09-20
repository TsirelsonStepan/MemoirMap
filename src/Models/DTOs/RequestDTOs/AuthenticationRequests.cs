namespace MemoirMap.Models.DTOs;

public class LogInRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class SignUpRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}