using ClassLibrary.DTOs.Sets.SetBonusEffectDto;
using ClassLibrary.DTOs.Sets.SetDto;

namespace ClassLibrary.DTOs.Sets.SetBonusDto;

public record AddSetBonusDto(
    int SetId,
    int NumberOfItems,
    List<AddSetBonusEffectDto> SetBonusEffects
);