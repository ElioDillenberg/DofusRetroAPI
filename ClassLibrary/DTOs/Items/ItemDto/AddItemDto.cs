namespace ClassLibrary.DTOs.Items.ItemDto;

public record AddItemDto(
    int Id,
    int Level,
    int ItemType,
    int Pods,
    int Image
);
