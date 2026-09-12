using MemoirMap.Models.DomainModels;
using MemoirMap.Models.DTOs;

namespace MemoirMap.Services;

public interface IAccountService
{
    public Task<ApplicationResult> DeleteAccount(string username);
}

public interface ILogInService
{
    public Task<ApplicationResult<string>> LogIn(string username, string password);
}

public interface ISignUpService
{
    public Task<ApplicationResult> SignUp(string username, string password);
}

public interface IEditExperienceService
{
    void CreateExperience(int location, ExperienceModel experience, string visibility);

    void AddExperienceToLocation(int locationId, ExperienceModel experience, string visibility);

    void UpdateExperience(int experienceId, ExperienceModel experience);

    void DeleteExperience(int experienceId);
}

public interface IGetExperienceService
{
    ExperienceModel GetExperience(int experienceId);

    ExperiencesResponse GetExperiences(ExperienceFiltersRequest filters, PaginationRequest pagination);
}

public interface ILocationService
{
    LocationModel GetLocation(int locationId);
    LocationsWithPreviewResponse GetLocations(LocationModel around, double distance, LocationFiltersRequest filters, PaginationRequest pagination);
}

public interface ISaveExperienceService
{
    void SaveExperience(int experienceId);
    void UnsaveExperience(int experienceId);
    ExperiencesResponse GetSaveExperiences(PaginationRequest pagination, ExperienceFiltersRequest filters);
}

public interface ISaveLocationService
{
    void SaveLocation(int locationId);
    void UnsaveLocation(int locationId);
    LocationsWithPreviewResponse GetSaveLocations(PaginationRequest pagination, ExperienceFiltersRequest filters);
}

public interface ISubscriptionService
{
    void SubscribeToUser(int userId);
    void UnsubscribeFromUser(int userId);
    LocationsWithPreviewResponse GetSubscriptions(PaginationRequest pagination);
}