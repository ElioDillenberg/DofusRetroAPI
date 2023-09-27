namespace DofusRetroClassLibrary.DTOs.Monsters.GenericMonster;

public record AddMonsterDto(
    int GameId,
    int EcosystemName,
    int BreedName
);