using ClassLibrary.DTOs.Sets.SetBonusDto;

namespace ClassLibrary.DTOs.Sets.SetDto;

public record GetSetDto(
    int Id,
    int[] EquipmentIds,
    string Name,
    GetSetBonusDto[]? SetBonuses
);