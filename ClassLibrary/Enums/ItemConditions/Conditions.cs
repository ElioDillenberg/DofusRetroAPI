namespace ClassLibrary.Enums.ItemConditions;

public static class ConditionStrings
{
    static ConditionStrings()
    {
        if (ConditionTypeStrings == null || ConditionTypeStrings.Count == 0)
            ConditionTypeStrings = new Dictionary<ConditionType, string>()
            {
                { ConditionType.Vitality, "CV" },
                { ConditionType.Wisdom, "CW" },
                { ConditionType.Strength, "CS" },
                { ConditionType.Intelligence, "CI" },
                { ConditionType.Chance, "CC" },
                { ConditionType.Agility, "CA" },
                { ConditionType.BaseVitality, "Cv" },
                { ConditionType.BaseWisdom, "Cw" },
                { ConditionType.BaseStrength, "Cs" },
                { ConditionType.BaseIntelligence, "Ci" },
                { ConditionType.BaseChance, "Cc" },
                { ConditionType.BaseAgility, "Ca" },
                { ConditionType.MovementPoint, "PM" },
                { ConditionType.CharacterLevel, "PL" },
                { ConditionType.Class, "PG" },
                { ConditionType.Job, "PJ" },
                { ConditionType.InZone, "PB" },
                { ConditionType.Alignment, "Ps" },
                { ConditionType.AlignmentLevel, "Pa" },
                { ConditionType.PvpRank, "PP" },
                { ConditionType.SubscribtionStatus, "PZ" },
                { ConditionType.Gift, "Pg" },
                { ConditionType.CharacterName, "PN" },
                { ConditionType.Gender, "PS" },
            };
        
        if (ConditionSignStrings == null || ConditionSignStrings.Count == 0)
            ConditionSignStrings = new Dictionary<ConditionSign, string>()
            {
                { ConditionSign.Equal, "=" },
                { ConditionSign.NotEqual, "!" },
                { ConditionSign.LessThan, "<" },
                { ConditionSign.MoreThan, ">" },
            };
    }

    public static readonly Dictionary<ConditionType, string> ConditionTypeStrings;

    public static readonly Dictionary<ConditionSign, string> ConditionSignStrings;
}