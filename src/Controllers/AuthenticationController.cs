using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;
using MemoirMap.Services;

namespace MemoirMap.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly ILogInService _logIn;
    private readonly ISignUpService _signUp;

    public AuthenticationController(ILogger<AuthenticationController> logger, ILogInService logInService, ISignUpService signUpService)
    {
        _logger = logger;
        _logIn = logInService;
        _signUp = signUpService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginRequest loginData)
    {
        ApplicationResult<string> result = await _logIn.LogIn(loginData.Username, loginData.Password);
        
        if (result.IsSuccess) return StatusCode(StatusCodes.Status200OK, result.Value);
        return StatusCode(ErrorToHttpStatusCodeMapper.HttpStatusCodeFromErrorCodes(result.Errors), result.Errors.Select(error => error.ToString()));
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] LoginRequest loginData)
    {
        ApplicationResult result = await _signUp.SignUp(loginData.Username, loginData.Password);

        if (result.IsSuccess) return StatusCode(StatusCodes.Status200OK);
        return StatusCode(ErrorToHttpStatusCodeMapper.HttpStatusCodeFromErrorCodes(result.Errors), result.Errors.Select(error => error.ToString()));
    }
}