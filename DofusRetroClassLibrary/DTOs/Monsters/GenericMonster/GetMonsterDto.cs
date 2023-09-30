using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;

namespace DofusRetroClassLibrary.DTOs.Monsters.GenericMonster;

public record GetMonsterDto(
    int Id,
    string Name,
    int Ecosystem,
    string EcosystemName,
    int Breed,
    string BreedName,
    List<GetMonsterCharacteristicDto> Characteristics
);