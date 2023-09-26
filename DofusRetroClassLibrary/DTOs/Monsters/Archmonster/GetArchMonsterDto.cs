using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;

namespace DofusRetroClassLibrary.DTOs.Monsters.Archmonster;

public record GetArchMonsterDto(
    int Id,
    int GameId,
    string Name,
    int Ecosystem,
    string EcosystemName,
    int Breed,
    string BreedName,
    List<GetMonsterCharacteristicDto> Characteristics,
    int MonsterId,
    string MonsterName
);