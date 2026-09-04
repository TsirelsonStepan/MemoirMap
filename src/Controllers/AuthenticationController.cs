using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;
using MemoirMap.Services.Interfaces;

namespace MemoirMap.Controllers;

[ApiController]
//[Produces("application/json")]
//[Consumes("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    [HttpPost("signin")]
    public SignInResult SignIn([FromBody] LoginRequest loginData)
    {
        //string jwt = _authenticationService.Login(loginData);
        //return Ok(jwt);
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest loginData)
    {
        ServiceResult result = await _authenticationService.Register(loginData.Username, loginData.Password);

        if (result.IsSuccess) return StatusCode(StatusCodes.Status201Created);
        return StatusCode(ErrorToHttpStatusCodeMapper.HttpStatusCodeFromErrorCodes(result.Errors), result.Errors);
    }
}