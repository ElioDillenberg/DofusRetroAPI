using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items.Equipments;

public class Effect
{
    // Id of the effect in the database
    public int Id { get; set; }
    
    // Id of the item in the database
    public int ItemId { get; set; }
    
    // Type of effect
    public EffectType EffectType { get; set; }
    
    // Minimal value of the effect
    public int Max { get; set; } 
    
    // Maximal value of the effect
    public int Min { get; set; }
}