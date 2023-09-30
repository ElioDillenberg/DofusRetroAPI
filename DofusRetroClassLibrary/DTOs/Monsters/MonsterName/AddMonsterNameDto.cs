namespace DofusRetroClassLibrary.DTOs.Monsters.MonsterName;

public record AddMonsterNameDto(
    int MonsterId,
    int LanguageId,
    string Name
);