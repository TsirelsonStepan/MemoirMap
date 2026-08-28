using MemoirMap.Models.DomainModels;

namespace MemoirMap.Models.DTOs;

public class CreateExperienceRequest
{
    public LocationModel? ExperienceLocation { get; set; }
    public string? Description { get; set; }
    public string? Visibility { get; set; }
}

public class UpdateExperienceRequest
{
    public string? Description { get; set; }
    public string? Visibility { get; set; }
}