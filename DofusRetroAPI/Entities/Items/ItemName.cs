using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items;

public sealed class ItemName : BaseLocalizedName
{
    public ItemName(Item item)
    {
        Item = item;
    }

    public Item Item { get; set; }
    public int ItemId { get; set; }
}