namespace DofusRetroClassLibrary.DTOs.Monsters.MonsterName;

public record AddMonsterNameDto(
    int MonsterGameId,
    int LanguageId,
    string Name
);