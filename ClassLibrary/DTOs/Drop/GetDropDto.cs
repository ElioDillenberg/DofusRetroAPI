namespace ClassLibrary.DTOs.Drop;

public record GetDropDto(
    int Id,
    int MonsterId,
    int ItemId,
    float? Rate,
    int? DropCap,
    int? ProspectionThreshold
);