namespace DofusRetroClassLibrary.DTOs.Monsters.Characteristics;

public record GetMonsterCharacteristicDto(
    int Id,
    int MonsterId,
    int Level,
    int HealthPoints,
    int ActionPoints,
    int MovementPoints,
    int Experience,
    int Initiative,
    int Strength,
    int Chance,
    int Intelligence,
    int Agility,
    int ActionPointAvoidance,
    int MovementPointAvoidance,
    int NeutralResistancePercentage,
    int EarthResistancePercentage,
    int FireResistancePercentage,
    int WaterResistancePercentage,
    int AirResistancePercentage
);