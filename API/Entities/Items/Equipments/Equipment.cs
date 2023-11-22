using DofusRetroAPI.Entities.Sets;

namespace DofusRetroAPI.Entities.Items.Equipments;

public abstract class Equipment : Item
{
    // The effects of the item
    // public List<EquipmentEffect> EquipmentEffects { get; set; } = new();
    
    // If the item is part of a set
    public Set? Set { get; set; }
    public int? SetId { get; set; }
}