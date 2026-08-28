using MemoirMap.Models.DTOs;
using MemoirMap.Models.DomainModels;

namespace MemoirMap.Services.Interfaces;

public interface IGetExperienceService
{
    ExperienceModel GetExperience(int experienceId);

    ExperiencesResponse GetExperiences(ExperienceFiltersRequest filters, PaginationRequest pagination);
}