using MemoirMap.Models.DomainModels;

namespace MemoirMap.Models.DTOs;

public class SubscriptionsResponse
{
    public List<UserModel> List { get; set; } = [];
    public int Count { get; set; }
}