using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items;

public sealed class ItemName : BaseLocalizedName
{
    public Item Item { get; set; } = null!;
    public int ItemId { get; set; }
}