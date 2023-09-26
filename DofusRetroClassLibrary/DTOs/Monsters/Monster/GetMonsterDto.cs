using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;

namespace DofusRetroClassLibrary.DTOs.Monsters.Monster;

public record GetMonsterDto(
    int Id,
    int GameId,
    string Name,
    int Ecosystem,
    string EcosystemName,
    int Breed,
    string BreedName,
    List<GetMonsterCharacteristicDto> Characteristics
);