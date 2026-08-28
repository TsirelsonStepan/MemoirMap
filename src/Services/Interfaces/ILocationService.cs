using MemoirMap.Models.DTOs;
using MemoirMap.Models.DomainModels;

namespace MemoirMap.Services.Interfaces;

public interface ILocationService
{
    LocationModel GetLocation(int locationId);
    LocationsWithPreviewResponse GetLocations(LocationModel around, double distance, LocationFiltersRequest filters, PaginationRequest pagination);
}