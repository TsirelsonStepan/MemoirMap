using MemoirMap.Models.DomainModels;

namespace MemoirMap.Models.DTOs;

public class CircleAreaRequest
{
    LocationModel? Around { get; set; }
    double? Distance { get; set; }
}