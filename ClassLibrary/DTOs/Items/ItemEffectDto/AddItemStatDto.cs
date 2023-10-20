namespace ClassLibrary.DTOs.Items.ItemEffectDto;

public record AddItemStatDto(
    int ItemId,
    int StatType,
    int StatMinValue,
    int StatMaxValue
);