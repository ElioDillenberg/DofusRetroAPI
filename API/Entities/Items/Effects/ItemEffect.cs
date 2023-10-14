using ClassLibrary.Enums.Effects;

namespace DofusRetroAPI.Entities.Items.Effects;

public sealed class ItemEffect
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    
    public EffectType EffectType { get; set; }
    
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}