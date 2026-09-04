using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Services.Interfaces;

namespace MemoirMap.Controllers;

[ApiController]
[Route("my")]
[Authorize]
//[Produces("application/json")]
//[Consumes("application/json")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpDelete("delete")]
    public StatusCodeResult DeleteAccount()
    {
        //_accountService.DeleteAccount();
        throw new NotImplementedException();
    }
}