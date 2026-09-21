namespace MemoirMap.Models.DTOs;

public class ErrorResponse
{
    public IEnumerable<string> Errors { get; set; } = [];
}