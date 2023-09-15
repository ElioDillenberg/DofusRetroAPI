using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Consumables.Categories;

public sealed class ConsumableCategoryName : BaseLocalizedName
{
    // Reference to the category
    public ConsumableCategory Category { get; set; } = null!;
    public int ConsumableCategoryId { get; set; }
}