namespace DofusRetroClassLibrary.DTOs.Monsters.MonsterNameDto;

public record AddMonsterNameDto(
    int MonsterId,
    int LanguageId,
    string Name
);