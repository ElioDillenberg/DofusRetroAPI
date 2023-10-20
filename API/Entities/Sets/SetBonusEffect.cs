using ClassLibrary.Enums.Stats;
using DofusRetroAPI.Entities.Items.Effects;

namespace DofusRetroAPI.Entities.Sets;

public class SetBonusEffect
{
    // Id of the effect in the database
    public int Id { get; set; }
    
    // Reference to the equipment
    public int SetBonusId { get; set; }
    public SetBonus SetBonus { get; set; } = null!;
    
    // Type of effect
    public StatType StatType { get; set; }
    
    // Minimal value of the effect
    public int Min { get; set; } 
    
    // Maximal value of the effect
    public int Max { get; set; }
}