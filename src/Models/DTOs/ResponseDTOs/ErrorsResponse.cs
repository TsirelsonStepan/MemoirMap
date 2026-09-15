namespace MemoirMap.Models.DTOs;

public class ErrorsResponse
{
    public IEnumerable<string> Errors { get; set; } = [];
}