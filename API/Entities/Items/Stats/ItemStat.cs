using ClassLibrary.Enums.Stats;

namespace DofusRetroAPI.Entities.Items.Effects;

public sealed class ItemStat
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    
    public StatType StatType { get; set; }
    
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}