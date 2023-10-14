namespace ClassLibrary.DTOs.Sets.SetBonusEffectDto;

public record AddSetBonusEffectDto(
    int EffectType,
    int MinValue,
    int MaxValue
);
