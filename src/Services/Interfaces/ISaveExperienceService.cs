using MemoirMap.Models.DTOs;

namespace MemoirMap.Services.Interfaces;

public interface ISaveExperienceService
{
    void SaveExperience(int experienceId);
    void UnsaveExperience(int experienceId);
    ExperiencesResponse GetSaveExperiences(PaginationRequest pagination, ExperienceFiltersRequest filters);
}