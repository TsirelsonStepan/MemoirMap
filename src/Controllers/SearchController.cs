using Microsoft.AspNetCore.Mvc;

using MemoirMap.Services.Interfaces;
using MemoirMap.Models.DTOs;

namespace MemoirMap.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ILocationService _locationService;
    private readonly IGetExperienceService _getExperienceService;

    public SearchController(ILogger<SearchController> logger, ILocationService locationService, IGetExperienceService getExperienceService)
    {
        _logger = logger;
        _locationService = locationService;
        _getExperienceService = getExperienceService;
    }

    [HttpGet("location/{locationId}")]
    public ObjectResult GetLocation([FromRoute] int locationId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("location")]
    public ObjectResult GetLocations([FromQuery] CircleAreaRequest area, [FromQuery] PaginationRequest pagination, [FromQuery] LocationFiltersRequest filters)
    {
        throw new NotImplementedException();
    }

    [HttpGet("experience/{experienceId}")]
    public ObjectResult GetExperience([FromRoute] int experienceId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("experience")]
    public ObjectResult GetExperiences([FromQuery] PaginationRequest pagination, [FromQuery] LocationFiltersRequest filters)
    {
        throw new NotImplementedException();
    }
}