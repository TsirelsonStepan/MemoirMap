using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;

namespace MemoirMap.Controllers;

[ApiController]
[Route("my/saved/locations")]
[Authorize]
public class MySavedLocationsController : ControllerBase
{
    private readonly ILogger<MySavedLocationsController> _logger;

    public MySavedLocationsController(ILogger<MySavedLocationsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ObjectResult GetSaveLocations([FromQuery] LocationFiltersRequest filters, [FromQuery] PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public StatusCodeResult SaveLocation([FromQuery] int locationId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{locationId}")]
    public StatusCodeResult UnsaveLocation([FromRoute] int locationId)
    {
        throw new NotImplementedException();
    }
}