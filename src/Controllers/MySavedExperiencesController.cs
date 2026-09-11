using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;

namespace MemoirMap.Controllers;

[ApiController]
[Route("my/saved/experiences")]
[Authorize]
public class MySavedExperiencesController : ControllerBase
{
    private readonly ILogger<MySavedExperiencesController> _logger;

    public MySavedExperiencesController(ILogger<MySavedExperiencesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ObjectResult GetSaveExperiences([FromQuery] ExperienceFiltersRequest filters, [FromQuery] PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public StatusCodeResult SaveExperience([FromQuery] int experienceId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{experienceId}")]
    public StatusCodeResult UnsaveExperience([FromRoute] int experienceId)
    {
        throw new NotImplementedException();
    }
}