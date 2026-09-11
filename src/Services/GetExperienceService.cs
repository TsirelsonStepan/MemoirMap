using MemoirMap.Models.DTOs;
using MemoirMap.Models.DomainModels;

namespace MemoirMap.Services;

public class GetExperienceService : IGetExperienceService
{
    public ExperienceModel GetExperience(int experienceId)
    {
        throw new NotImplementedException();
    }

    public ExperiencesResponse GetExperiences(ExperienceFiltersRequest filters, PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }
}