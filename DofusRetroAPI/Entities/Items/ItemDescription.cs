using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items;

public class ItemDescription
{
    public int Id { get; set; }
    
    public Item Item { get; set; }
    public int ItemId { get; set; }
    
    public Language Language { get; set; }

    public string Description { get; set; } = string.Empty;
}