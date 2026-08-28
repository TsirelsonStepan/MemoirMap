using MemoirMap.Models.DTOs;
using MemoirMap.Models.DomainModels;
using MemoirMap.Services.Interfaces;

namespace MemoirMap.Services;

public class LocationService : ILocationService
{
    public LocationModel GetLocation(int locationId)
    {
        throw new NotImplementedException();
    }

    public LocationsWithPreviewResponse GetLocations(LocationModel around, double distance, LocationFiltersRequest filters, PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }
}