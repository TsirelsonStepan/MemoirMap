using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Services;

namespace MemoirMap.Controllers;

[ApiController]
[Route("my/account")]
[Authorize]
public class MyAccountController : ControllerBase
{
    private readonly ILogger<MyAccountController> _logger;
    private readonly IAccountService _account;

    public MyAccountController(ILogger<MyAccountController> logger, IAccountService account)
    {
        _logger = logger;
        _account = account;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException();
        ApplicationResult result = await _account.DeleteAccount(userId);
        if (result.IsSuccess) return StatusCode(StatusCodes.Status200OK);
        return StatusCode(ErrorToHttpStatusCodeMapper.HttpStatusCodeFromErrorCodes(result.Errors), result.Errors.Select(error => error.ToString()));
    }
}