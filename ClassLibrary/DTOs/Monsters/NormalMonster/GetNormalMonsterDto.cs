using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;

namespace DofusRetroClassLibrary.DTOs.Monsters.NormalMonster;

public record GetNormalMonsterDto(
    int Id,
    string Name,
    int Ecosystem,
    string EcosystemName,
    int Breed,
    string BreedName,
    List<GetMonsterCharacteristicDto> Characteristics,
    int? ArchMonsterId,
    string? ArchMonsterName
);