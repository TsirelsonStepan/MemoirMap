using MemoirMap.Models.DomainModels;

namespace MemoirMap.Models.DTOs;

public class LocationsResponse
{
    public List<LocationModel> List { get; set; } = [];
    public int Count { get; set; }
}

public class LocationsWithPreviewResponse
{
    public LocationsResponse? Locations { get; set; }
    public ExperiencesResponse? Previews { get; set; }
}