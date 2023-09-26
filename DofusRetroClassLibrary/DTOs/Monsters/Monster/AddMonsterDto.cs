namespace DofusRetroClassLibrary.DTOs.Monsters.Monster;

public record AddMonsterDto(
    int GameId,
    int LanguageId,
    int EcosystemName,
    int BreedName
);