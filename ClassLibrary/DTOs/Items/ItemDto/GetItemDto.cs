namespace ClassLibrary.DTOs.Items.ItemDto;

public record GetItemDto(
    int Id,
    int ItemType,
    int Level,
    int Pods,
    int? Image
);
