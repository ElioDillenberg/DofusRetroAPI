using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Consumables.Categories;

public sealed class ConsumableCategoryName : BaseLocalizedName
{

    // Reference to the category
    public ConsumableCategory ConsumableCategory { get; set; }
    public int ConsumableCategoryId { get; set; }
}