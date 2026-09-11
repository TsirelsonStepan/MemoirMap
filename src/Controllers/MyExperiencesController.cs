using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DomainModels;
using MemoirMap.Models.DTOs;

namespace MemoirMap.Controllers;

[ApiController]
[Route("my/experiences")]
[Authorize]
public class MyExperiencesController : ControllerBase
{
    private readonly ILogger<MyExperiencesController> _logger;

    public MyExperiencesController(ILogger<MyExperiencesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ObjectResult GetMyExperiences([FromQuery] ExperienceFiltersRequest filters, [FromQuery] PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{latitude}:{longitude}")]
    public StatusCodeResult CreateExperience([FromRoute] double latitude, [FromRoute] double longitude, [FromBody] ExperienceModel experience)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{locationId}")]
    public StatusCodeResult AddExperience([FromRoute] string locationId, [FromBody] ExperienceModel experience)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{experienceId}")]
    public StatusCodeResult UpdateExperience([FromRoute] int experienceId, [FromBody] UpdateExperienceRequest experience)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{experienceId}")]
    public StatusCodeResult DeleteExperience([FromRoute] int experienceId)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{experienceId}")]
    public ObjectResult GetMyExperience([FromRoute] int experienceId)
    {
        throw new NotImplementedException();
    }
}