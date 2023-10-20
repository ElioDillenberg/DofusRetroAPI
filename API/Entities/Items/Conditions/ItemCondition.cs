using System.Text;
using ClassLibrary.DTOs.Items.ItemConditionDto;
using ClassLibrary.Enums.Alignments;
using ClassLibrary.Enums.Classes;
using ClassLibrary.Enums.Genders;
using ClassLibrary.Enums.ItemConditions;
using ClassLibrary.Enums.Languages;
using ClassLibrary.Enums.Stats;
using DofusRetroAPI.Localization;

namespace DofusRetroAPI.Entities.Items.Conditions;

public sealed class ItemCondition
{
    public int Id { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public ConditionType ConditionType { get; set; }
    
    public ConditionSign ConditionSign { get; set; }
    
    public int Value { get; set; }
    
    public List<ItemConditionText> ConditionTexts { get; set; } = new();
}

public static class ItemConditionExtensions
{
    public static GetItemConditionDto AsGetItemConditionDto(this ItemCondition itemCondition, int languageId)
    {
        ItemConditionText? conditionText = itemCondition.ConditionTexts.FirstOrDefault(ct => ct.Language == (Language)languageId);
        
        return new GetItemConditionDto(
            Id: itemCondition.Id,
            ItemId: itemCondition.ItemId,
            ConditionType: (int)itemCondition.ConditionType,
            ConditionSign : (int)itemCondition.ConditionSign,
            Value: itemCondition.Value,
            Text: conditionText == null ? "" : conditionText.Text
        );
    }

    public static string BuildStringCondition(this ItemCondition itemCondition, Language language)
    {
        // Check if language Id exists, otherwise return empty string
        if (!Enum.IsDefined(typeof(Language), language))
            return "";
        
        StringBuilder stringBuilder = new StringBuilder();
        
        // add the ConditionType
        stringBuilder.Append(BuildConditionTypeString(itemCondition.ConditionType, language));
        
        // add the Condition Sign
        stringBuilder.Append($" {ConditionStrings.ConditionSignStrings[itemCondition.ConditionSign]} ");
        
        // add the Condition Value
        stringBuilder.Append(BuildConditionValueString(itemCondition.ConditionType, itemCondition.Value, language));
        
        return stringBuilder.ToString();
    }

    private static string BuildConditionValueString(ConditionType conditionType, int itemConditionValue, Language language)
    {
        string conditionValueString = "";
        switch (conditionType)
        {
            case ConditionType.Agility:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Chance:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Intelligence:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Strength:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Vitality:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Wisdom:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.BaseAgility:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.BaseChance:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.BaseIntelligence:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.BaseStrength:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.BaseVitality:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.BaseWisdom:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.MovementPoint:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.CharacterLevel:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Class:
                conditionValueString = LocalizedStrings.PlayableClassNames![new ValueTuple<PlayableClass, Language>((PlayableClass)itemConditionValue, language)];
                break;
            case ConditionType.InZone:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Alignment:
                conditionValueString = LocalizedStrings.AlignmentNames![new ValueTuple<Alignment, Language>((Alignment)itemConditionValue, language)];
                break;
            case ConditionType.AlignmentLevel:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.PvpRank:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.SubscribtionStatus:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Gift:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.CharacterName:
                conditionValueString = itemConditionValue.ToString();
                break;
            case ConditionType.Gender:
                conditionValueString = LocalizedStrings.GenderNames![new ValueTuple<Gender, Language>((Gender)itemConditionValue, language)];
                break;
            default:
                conditionValueString = "Unknown condition type";
                break;
        }
        return conditionValueString;
    }
    
    private static string BuildConditionTypeString(ConditionType conditionType, Language language)
    {
        string conditionTypeString = "";
        switch (conditionType)
        {
            case ConditionType.Vitality:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Vitality, language)];
                break;
            case ConditionType.Wisdom:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Wisdom, language)];
                break;
            case ConditionType.Strength:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Strength, language)];
                break;
            case ConditionType.Intelligence:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Intelligence, language)];
                break;
            case ConditionType.Chance:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Chance, language)];
                break;
            case ConditionType.Agility:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Agility, language)];
                break;
            case ConditionType.BaseVitality:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Vitality, language)];
                break;
            case ConditionType.BaseWisdom:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Wisdom, language)];
                break;
            case ConditionType.BaseStrength:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Strength, language)];
                break;
            case ConditionType.BaseIntelligence:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Intelligence, language)];
                break;
            case ConditionType.BaseChance:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Chance, language)];
                break;
            case ConditionType.BaseAgility:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.Agility, language)];
                break;
            case ConditionType.MovementPoint:
                conditionTypeString = LocalizedStrings.StatNames![new ValueTuple<StatType, Language>(StatType.MovementPoint, language)];
                break;
            case ConditionType.CharacterLevel:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.CharacterLevel, language)];
                break;
            case ConditionType.Class:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.Class, language)];
                break;
            case ConditionType.InZone:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.InZone, language)];
                break;
            case ConditionType.Alignment:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.Alignment, language)];
                break;
            case ConditionType.AlignmentLevel:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.AlignmentLevel, language)];
                break;
            case ConditionType.PvpRank:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.PvpRank, language)];
                break;
            case ConditionType.SubscribtionStatus:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.SubscribtionStatus, language)];
                break;
            case ConditionType.Gift:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.Gift, language)];
                break;
            case ConditionType.CharacterName:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.CharacterName, language)];
                break;
            case ConditionType.Gender:
                conditionTypeString = LocalizedStrings.ConditionTypeNames![(ConditionType.Gender, language)];
                break;
            default:
                conditionTypeString = "Unknown condition type";
                break;
        }
        return conditionTypeString;
    }
}