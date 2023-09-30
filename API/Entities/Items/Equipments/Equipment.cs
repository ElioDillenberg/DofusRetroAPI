using DofusRetroAPI.Entities.Items.Equipments.Sets;

namespace DofusRetroAPI.Entities.Items.Equipments;

public abstract class Equipment : Item
{
    public virtual EquipmentType EquipmentType { get; set; }

    // The constraints to equip the item
    public List<EquipmentCondition> EquipmentConditions { get; set; } = new();
    
    // The effects of the item
    public List<EquipmentEffect> EquipmentEffects { get; set; } = new();
    
    // If the item is part of a set
    public Set? Set { get; set; }
    public int? SetId { get; set; }
}