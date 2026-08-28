using MemoirMap.Models.DomainModels;

namespace MemoirMap.Services.Interfaces;

public interface IEditExperienceService
{
    void CreateExperience(int location, ExperienceModel experience, string visibility);

    void AddExperienceToLocation(int locationId, ExperienceModel experience, string visibility);

    void UpdateExperience(int experienceId, ExperienceModel experience);

    void DeleteExperience(int experienceId);
}