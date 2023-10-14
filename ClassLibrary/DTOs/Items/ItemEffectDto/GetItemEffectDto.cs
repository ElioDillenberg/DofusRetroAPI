namespace ClassLibrary.DTOs.Items.ItemEffectDto;

public record GetItemEffectDto(
    int Id,
    int ItemId,
    int EffectType,
    int MinValue,
    int MaxValue
);