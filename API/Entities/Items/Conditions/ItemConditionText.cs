using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Conditions;

public class ItemConditionText : BaseLocalizedName
{
    public ItemCondition ItemCondition { get; set; } = null!;
    public int ItemConditionId { get; set; }
}