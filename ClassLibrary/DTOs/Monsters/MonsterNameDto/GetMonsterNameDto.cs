namespace DofusRetroClassLibrary.DTOs.Monsters.MonsterNameDto;

public record GetMonsterNameDto(
    int Id,
    int MonsterId,
    int LanguageId,
    string Name
);