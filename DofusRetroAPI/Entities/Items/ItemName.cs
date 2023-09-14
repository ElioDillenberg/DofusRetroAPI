using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public class ItemName
{
    public int Id { get; set; }
    
    public Item Item { get; set; } = null!;
    public int ItemId { get; set; }
    
    public Language Language { get; set; }

    public string Name { get; set; } = string.Empty;
}