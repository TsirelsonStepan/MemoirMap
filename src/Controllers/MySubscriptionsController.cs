using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Models.DTOs;

namespace MemoirMap.Controllers;

[ApiController]
[Route("my/subscription")]
[Authorize]
public class MySubscriptionController : ControllerBase
{
    private readonly ILogger<MySubscriptionController> _logger;

    public MySubscriptionController(ILogger<MySubscriptionController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ObjectResult GetSubscriptions([FromQuery] PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public StatusCodeResult SubscribeToUser([FromQuery] int userID)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{subscriptionId}")]
    public StatusCodeResult UnsubscribeFromUser([FromRoute] int subscriptionId)
    {
        throw new NotImplementedException();
    }
}