using MemoirMap.Models.DTOs;

namespace MemoirMap.Services.Interfaces;

public interface ISaveLocationService
{
    void SaveLocation(int locationId);
    void UnsaveLocation(int locationId);
    LocationsWithPreviewResponse GetSaveLocations(PaginationRequest pagination, ExperienceFiltersRequest filters);
}