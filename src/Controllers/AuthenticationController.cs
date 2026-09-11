using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;
using MemoirMap.Services;

namespace MemoirMap.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginRequest loginData)
    {
        ApplicationResult<string> result = await _authenticationService.SignIn(loginData.Username, loginData.Password);
        
        if (result.IsSuccess) return StatusCode(StatusCodes.Status200OK, result.Value);
        return StatusCode(ErrorToHttpStatusCodeMapper.HttpStatusCodeFromErrorCodes(result.Errors), result.Errors.Select(error => error.ToString()));
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] LoginRequest loginData)
    {
        ApplicationResult result = await _authenticationService.Register(loginData.Username, loginData.Password);

        if (result.IsSuccess) return StatusCode(StatusCodes.Status200OK);
        return StatusCode(ErrorToHttpStatusCodeMapper.HttpStatusCodeFromErrorCodes(result.Errors), result.Errors.Select(error => error.ToString()));
    }
}