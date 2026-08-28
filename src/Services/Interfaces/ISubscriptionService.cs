using MemoirMap.Models.DTOs;

namespace MemoirMap.Services.Interfaces;

public interface ISubscriptionService
{
    void SubscribeToUser(int userId);
    void UnsubscribeFromUser(int userId);
    LocationsWithPreviewResponse GetSubscriptions(PaginationRequest pagination);
}