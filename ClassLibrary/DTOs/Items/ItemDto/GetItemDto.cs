using ClassLibrary.DTOs.Items.ItemConditionDto;
using ClassLibrary.DTOs.Items.ItemEffectDto;

namespace ClassLibrary.DTOs.Items.ItemDto;

public record GetItemDto(
    int Id,
    string Name,
    string Description,
    int ItemType,
    int Level,
    int Pods,
    int Price,
    int? Image,
    List<GetItemConditionDto>? Conditions,
    List<GetItemStatDto>? Stats
);
