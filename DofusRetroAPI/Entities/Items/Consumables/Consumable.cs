using DofusRetroAPI.Entities.Items.Consumables.Categories;

namespace DofusRetroAPI.Entities.Items.Consumables;

public sealed class Consumable : Item
{
    public ConsumableCategory ConsumableCategory { get; set; } = null!;
    public int ConsumableCategoryId { get; set; }
}