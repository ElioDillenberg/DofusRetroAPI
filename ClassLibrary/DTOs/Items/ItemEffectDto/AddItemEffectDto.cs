namespace ClassLibrary.DTOs.Items.ItemEffectDto;

public record AddItemEffectDto(
    int ItemId,
    int EffectType,
    int EffectMinValue,
    int EffectMaxValue
);