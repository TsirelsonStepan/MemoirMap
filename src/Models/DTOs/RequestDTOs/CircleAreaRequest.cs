using MemoirMap.Models.DomainModels;

namespace MemoirMap.Models.DTOs;

public struct CircleAreaRequest
{
    LocationModel? Around { get; set; }
    double? Distance { get; set; }
}