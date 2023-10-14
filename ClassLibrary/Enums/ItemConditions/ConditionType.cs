namespace ClassLibrary.Enums.ItemConditions;

public enum ConditionType
{
    Vitality = 1,
    Wisdom = 2,
    Strength = 3,
    Intelligence = 4,
    Chance = 5,
    Agility = 6,
    BaseVitality = 7,
    BaseWisdom = 8,
    BaseStrength = 9,
    BaseIntelligence = 10,
    BaseChance = 11,
    BaseAgility = 12,
    MovementPoint = 13,
    CharacterLevel = 14,
    Class = 15, // See PlayableClasses enum
    Job = 16, // See Jobs enum
    InZone = 17, // id => SubArea Id (enum)
    Alignment = 18, // Alignment -> 0 = neutre, 1 = bonta, 2 = brakmar, 3 = mercenaire
    AlignmentLevel = 19, // Quetes alignement
    PvpRank = 20, // Ailes pvp
    SubscribtionStatus = 21, //  1 = abo
    Gift = 22, // Alignment gifts
    CharacterName = 23, // Tricky as it is a string, don't handle it for now, pretend there is no condition
    Gender = 24
}