using MemoirMap.Models.DTOs;
using MemoirMap.Services.Interfaces;

namespace MemoirMap.Services;

public class SubscriptionService : ISubscriptionService
{
    public LocationsWithPreviewResponse GetSubscriptions(PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }

    public void SubscribeToUser(int userId)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeFromUser(int userId)
    {
        throw new NotImplementedException();
    }
}