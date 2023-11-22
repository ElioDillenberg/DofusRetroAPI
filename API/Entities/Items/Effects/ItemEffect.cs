using ClassLibrary.Enums.ItemEffects;

namespace DofusRetroAPI.Entities.Items.Effects;

public class ItemEffect
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    
    public ItemEffectType EffectType { get; set; }
    
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}