namespace DofusRetroClassLibrary.DTOs.Monsters.MonsterName;

public record GetMonsterNameDto(
    int Id,
    int MonsterId,
    int LanguageId,
    string Name
);