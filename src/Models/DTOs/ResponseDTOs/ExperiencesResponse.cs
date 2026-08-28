using MemoirMap.Models.DomainModels;

namespace MemoirMap.Models.DTOs;

public class ExperiencesResponse
{
    public List<ExperienceModel> List { get; set; } = [];
    public int Count { get; set; }
}