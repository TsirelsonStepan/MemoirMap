using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Services.Interfaces;
using MemoirMap.Models.DTOs;

namespace Controllers;

[ApiController]
[Route("my/save/experience")]
[Authorize]
public class SaveExperienceController : ControllerBase
{
    private readonly ILogger<SaveExperienceController> _logger;
    private readonly ISaveExperienceService _saveExperienceService;

    public SaveExperienceController(ILogger<SaveExperienceController> logger, ISaveExperienceService saveExperienceService)
    {
        _logger = logger;
        _saveExperienceService = saveExperienceService;
    }

    [HttpPost("save")]
    public StatusCodeResult SaveExperience([FromQuery] int experienceId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("unsave")]
    public StatusCodeResult UnsaveExperience([FromQuery] int experienceId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public ObjectResult GetSaveExperiences([FromQuery] ExperienceFiltersRequest filters, [FromQuery] PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }
}