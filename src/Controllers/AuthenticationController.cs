using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;
using MemoirMap.Services;

namespace MemoirMap.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly ILogInService _logInService;
    private readonly ISignUpService _signUpService;

    public AuthenticationController(ILogger<AuthenticationController> logger, ILogInService logInService, ISignUpService signUpService)
    {
        _logger = logger;
        _logInService = logInService;
        _signUpService = signUpService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginRequest loginData)
    {
        ApplicationResult<string> result = await _logInService.LogIn(loginData.Username, loginData.Password);
        
        if (result.IsSuccess) return StatusCode(StatusCodes.Status200OK, result.Value);
        return StatusCode(ErrorToHttpStatusCodeMapper.HttpStatusCodeFromErrorCodes(result.Errors), result.Errors.Select(error => error.ToString()));
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] LoginRequest loginData)
    {
        ApplicationResult result = await _signUpService.SignUp(loginData.Username, loginData.Password);

        if (result.IsSuccess) return StatusCode(StatusCodes.Status200OK);
        return StatusCode(ErrorToHttpStatusCodeMapper.HttpStatusCodeFromErrorCodes(result.Errors), result.Errors.Select(error => error.ToString()));
    }
}