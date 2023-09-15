using DofusRetroAPI.Entities.Effects;

namespace DofusRetroAPI.Entities.Items.Equipments.Sets;

public class SetEffect
{
    // Id of the effect in the database
    public int Id { get; set; }
    
    // Reference to the equipment
    public Set Set { get; set; }
    public int SetId { get; set; }
    
    // Type of effect
    public EffectType EffectType { get; set; }
    
    // Minimal value of the effect
    public int Value { get; set; } 
}