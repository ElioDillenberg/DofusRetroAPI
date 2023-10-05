namespace ClassLibrary.DTOs.Items.CardDto;

public record AddCardDto(
    int Id,
    int Level,
    int ItemType,
    int Pods,
    int Image,
    int CardNumber,
    int CardFamily
);