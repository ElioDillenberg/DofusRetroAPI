﻿using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;

namespace DofusRetroAPI.Entities.Monsters;

public static class MonsterEntityExtensions
{
    public static GetMonsterCharacteristicDto AsGetMonsterCharacteristicDto(
        this MonsterCharacteristic monsterCharacteristic)
    {
        return new GetMonsterCharacteristicDto(
            Id: monsterCharacteristic.Id,
            Rank: monsterCharacteristic.Rank,
            MonsterId: monsterCharacteristic.MonsterId,
            Level: monsterCharacteristic.Level,
            HealthPoints: monsterCharacteristic.HealthPoints,
            ActionPoints: monsterCharacteristic.ActionPoints,
            MovementPoints: monsterCharacteristic.MovementPoints,
            Experience: monsterCharacteristic.Experience,
            Initiative: monsterCharacteristic.Initiative,
            Strength: monsterCharacteristic.Strength,
            Chance: monsterCharacteristic.Chance,
            Intelligence: monsterCharacteristic.Intelligence,
            Agility: monsterCharacteristic.Agility,
            ActionPointAvoidance: monsterCharacteristic.ActionPointAvoidance,
            MovementPointAvoidance: monsterCharacteristic.MovementPointAvoidance,
            NeutralResistancePercentage: monsterCharacteristic.NeutralResistancePercentage,
            EarthResistancePercentage: monsterCharacteristic.EarthResistancePercentage,
            FireResistancePercentage: monsterCharacteristic.FireResistancePercentage,
            WaterResistancePercentage: monsterCharacteristic.WaterResistancePercentage,
            AirResistancePercentage: monsterCharacteristic.AirResistancePercentage
        );
    }

    public static MonsterCharacteristic AsMonsterCharacteristic(
        this AddMonsterCharacteristicDto addMonsterCharacteristicDto, int monsterId)
    {
        return new MonsterCharacteristic()
        {
            Rank = addMonsterCharacteristicDto.Rank,
            MonsterId = monsterId,
            Level = addMonsterCharacteristicDto.Level,
            HealthPoints = addMonsterCharacteristicDto.HealthPoints,
            ActionPoints = addMonsterCharacteristicDto.ActionPoints,
            MovementPoints = addMonsterCharacteristicDto.MovementPoints,
            Experience = addMonsterCharacteristicDto.Experience,
            Initiative = addMonsterCharacteristicDto.Initiative,
            Strength = addMonsterCharacteristicDto.Strength,
            Chance = addMonsterCharacteristicDto.Chance,
            Intelligence = addMonsterCharacteristicDto.Intelligence,
            Agility = addMonsterCharacteristicDto.Agility,
            ActionPointAvoidance = addMonsterCharacteristicDto.ActionPointAvoidance,
            MovementPointAvoidance = addMonsterCharacteristicDto.MovementPointAvoidance,
            NeutralResistancePercentage = addMonsterCharacteristicDto.NeutralResistancePercentage,
            EarthResistancePercentage = addMonsterCharacteristicDto.EarthResistancePercentage,
            FireResistancePercentage = addMonsterCharacteristicDto.FireResistancePercentage,
            WaterResistancePercentage = addMonsterCharacteristicDto.WaterResistancePercentage,
            AirResistancePercentage = addMonsterCharacteristicDto.AirResistancePercentage
        };
    }
    
    public static GetLocalizedStringDto AsGetMonsterNameDto(this MonsterName monsterName)
    {
        return new GetLocalizedStringDto(
            Id: monsterName.Id,
            EntityId: monsterName.MonsterId,
            LanguageId: (int)monsterName.Language,
            Name: monsterName.Text
        );
    }
}