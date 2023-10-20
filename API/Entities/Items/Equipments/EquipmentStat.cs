using ClassLibrary.Enums.Stats;
using DofusRetroAPI.Entities.Items.Effects;

namespace DofusRetroAPI.Entities.Items.Equipments;

public class EquipmentEffect
{
    // Id of the effect in the database
    public int Id { get; set; }
    
    // Reference to the equipment
    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;
    
    // Type of effect
    public StatType StatType { get; set; }
    
    // Minimal value of the effect
    public int Max { get; set; } 
    
    // Maximal value of the effect
    public int Min { get; set; }
}