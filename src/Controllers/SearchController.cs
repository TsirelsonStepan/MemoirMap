using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;

namespace MemoirMap.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;

    public SearchController(ILogger<SearchController> logger)
    {
        _logger = logger;
    }

    [HttpGet("locations/{locationId}")]
    public ObjectResult GetLocation([FromRoute] int locationId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("locations")]
    public ObjectResult GetLocations([FromQuery] CircleAreaRequest area, [FromQuery] PaginationRequest pagination, [FromQuery] LocationFiltersRequest filters)
    {
        throw new NotImplementedException();
    }

    [HttpGet("experiences/{experienceId}")]
    public ObjectResult GetExperience([FromRoute] int experienceId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("experiences")]
    public ObjectResult GetExperiences([FromQuery] PaginationRequest pagination, [FromQuery] LocationFiltersRequest filters)
    {
        throw new NotImplementedException();
    }
}