namespace ClassLibrary.DTOs.Items.ItemEffectDto;

public record GetItemStatDto(
    int Id,
    int ItemId,
    int StatType,
    int MinValue,
    int MaxValue
);