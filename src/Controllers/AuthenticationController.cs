using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;
using MemoirMap.Services.Interfaces;

namespace Controllers;

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
    public ObjectResult SignIn([FromBody] LoginRequest loginData)
    {
        //string jwt = _authenticationService.Login(loginData);
        //return Ok(jwt);
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public StatusCodeResult Register([FromBody] LoginRequest loginData)
    {
        //_authenticationService.Register(loginData);
        //return new StatusCodeResult(200);
        throw new NotImplementedException();
    }
}