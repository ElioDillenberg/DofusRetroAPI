using ClassLibrary.DTOs.Sets.SetBonusEffectDto;
using ClassLibrary.DTOs.Sets.SetDto;

namespace ClassLibrary.DTOs.Sets.SetBonusDto;

public record GetSetBonusDto(
    int Id,
    int SetId,
    int NumberOfItems,
    List<GetSetBonusEffectDto> SetBonusEffects
);