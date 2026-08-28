using MemoirMap.Models.DTOs;
using MemoirMap.Services.Interfaces;

namespace MemoirMap.Services;

public class SaveExperienceService : ISaveExperienceService
{
    public ExperiencesResponse GetSaveExperiences(PaginationRequest pagination, ExperienceFiltersRequest filters)
    {
        throw new NotImplementedException();
    }

    public void SaveExperience(int experienceId)
    {
        throw new NotImplementedException();
    }

    public void UnsaveExperience(int experienceId)
    {
        throw new NotImplementedException();
    }
}