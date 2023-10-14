namespace ClassLibrary.DTOs.Items.ItemConditionDto;

public record GetItemConditionDto(
    int Id,
    int ItemId,
    int ConditionType,
    int ConditionSign,
    int Value,
    string Text
);