using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MemoirMap.Services.Interfaces;
using MemoirMap.Models.DTOs;

namespace Controllers;

[ApiController]
[Route("my/subscription")]
[Authorize]
public class SubscriptionController : ControllerBase
{
    private readonly ILogger<SubscriptionController> _logger;
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ILogger<SubscriptionController> logger, ISubscriptionService subscriptionService)
    {
        _logger = logger;
        _subscriptionService = subscriptionService;
    }

    [HttpPost("subscribe")]
    public StatusCodeResult SubscribeToUser([FromQuery] int userID)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("unsubscribe")]
    public StatusCodeResult UnsubscribeFromUser([FromQuery] int userID)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public ObjectResult GetSubscriptions([FromQuery] PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }
}