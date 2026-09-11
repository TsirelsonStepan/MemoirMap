using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoirMap.Controllers;

[ApiController]
[Route("my/account")]
[Authorize]
public class MyAccountController : ControllerBase
{
    private readonly ILogger<MyAccountController> _logger;

    public MyAccountController(ILogger<MyAccountController> logger)
    {
        _logger = logger;
    }

    [HttpDelete]
    public StatusCodeResult DeleteAccount()
    {
        throw new NotImplementedException();
    }
}