using MemoirMap.Models.DTOs;
using MemoirMap.Services.Interfaces;

namespace MemoirMap.Services;

public class SaveLocationService : ISaveLocationService
{
    public LocationsWithPreviewResponse GetSaveLocations(PaginationRequest pagination, ExperienceFiltersRequest filters)
    {
        throw new NotImplementedException();
    }

    public void SaveLocation(int locationId)
    {
        throw new NotImplementedException();
    }

    public void UnsaveLocation(int locationId)
    {
        throw new NotImplementedException();
    }
}