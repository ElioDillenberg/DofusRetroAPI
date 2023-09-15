using DofusRetroAPI.Entities.Effects;

namespace DofusRetroAPI.Entities.Items.Equipments;

public class EquipmentEffect
{
    // Id of the effect in the database
    public int Id { get; set; }
    
    // Reference to the equipment
    public Equipment Equipment { get; set; }
    public int EquipmentId { get; set; }
    
    // Type of effect
    public EffectType EffectType { get; set; }
    
    // Minimal value of the effect
    public int Max { get; set; } 
    
    // Maximal value of the effect
    public int Min { get; set; }
}