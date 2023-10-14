namespace ClassLibrary.DTOs.Items.ItemDto;

public record AddItemDto(
    int Id,
    int ItemType,
    int Level,
    int Pods,
    int Price,
    int Image
);
