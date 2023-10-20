using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Conditions;

public class ItemConditionText : BaseLocalizedText
{
    public ItemCondition ItemCondition { get; set; } = null!;
    public int ItemConditionId { get; set; }
}