namespace ClassLibrary.DTOs.Sets.SetBonusEffectDto;

public record GetSetBonusEffectDto(
    int Id,
    int SetBonusId,
    int EffectType,
    int MinValue,
    int MaxValue
);