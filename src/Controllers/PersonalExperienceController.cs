using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DomainModels;
using MemoirMap.Services.Interfaces;
using MemoirMap.Models.DTOs;

namespace Controllers;

[ApiController]
[Route("my/experience")]
[Authorize]
public class PersonalExperienceController : ControllerBase
{
    private readonly ILogger<PersonalExperienceController> _logger;
    private readonly IEditExperienceService _editExperienceService;
    private readonly IGetExperienceService _getExperienceService;

    public PersonalExperienceController(ILogger<PersonalExperienceController> logger, IEditExperienceService editExperienceService, IGetExperienceService getExperienceService)
    {
        _logger = logger;
        _editExperienceService = editExperienceService;
        _getExperienceService = getExperienceService;
    }

    [HttpPost("create")]
    public StatusCodeResult CreateExperience([FromBody] CreateExperienceRequest experience)
    {
        throw new NotImplementedException();
    }

    [HttpPost("add")]
    public StatusCodeResult AddExperience([FromBody] ExperienceModel experience)
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

    [HttpGet]
    public ObjectResult GetMyExperiences([FromQuery] ExperienceFiltersRequest filters, [FromQuery] PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }
}