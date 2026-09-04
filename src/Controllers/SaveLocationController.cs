using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Services.Interfaces;
using MemoirMap.Models.DTOs;

namespace MemoirMap.sControllers;

[ApiController]
[Route("my/save/location")]
[Authorize]
public class SaveLocationController : ControllerBase
{
    private readonly ILogger<SaveLocationController> _logger;
    private readonly ISaveLocationService _saveLocationService;

    public SaveLocationController(ILogger<SaveLocationController> logger, ISaveLocationService saveLocationService)
    {
        _logger = logger;
        _saveLocationService = saveLocationService;
    }

    [HttpPost("save")]
    public StatusCodeResult SaveLocation([FromQuery] int locationId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("unsave")]
    public StatusCodeResult UnsaveLocation([FromQuery] int locationId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public ObjectResult GetSaveLocations([FromQuery] LocationFiltersRequest filters, [FromQuery] PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }
}