namespace DofusRetroClassLibrary.DTOs.Monsters.NormalMonster;

public record AddNormalMonsterDto(
    int GameId,
    int LanguageId,
    int Ecosystem,
    int Breed
);