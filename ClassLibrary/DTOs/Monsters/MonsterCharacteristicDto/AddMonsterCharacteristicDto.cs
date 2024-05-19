namespace ClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;

public record AddMonsterCharacteristicDto(
    int Rank,
    int MonsterId,
    int Level,
    int? HealthPoints,
    int? ActionPoints,
    int? MovementPoints,
    int? Experience,
    int? Initiative,
    int? Strength,
    int? Intelligence,
    int? Chance,
    int? Agility,
    int? ActionPointAvoidance,
    int? MovementPointAvoidance,
    int? NeutralResistancePercentage,
    int? EarthResistancePercentage,
    int? FireResistancePercentage,
    int? WaterResistancePercentage,
    int? AirResistancePercentage
);