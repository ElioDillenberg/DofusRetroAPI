using ClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;

namespace DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;

public record GetMonsterDto(
    int Id,
    string Name,
    int Ecosystem,
    string EcosystemName,
    int Breed,
    string BreedName,
    int? RelatedMonsterId,
    string? RelatedMonsterName,
    List<GetMonsterCharacteristicDto> Characteristics,
    int? Image
);