namespace ClassLibrary.DTOs.Items.ItemConditionDto;

public record AddItemConditionDto(
    int ItemId,
    int ConditionType,
    int ConditionSignType,
    int Value
);