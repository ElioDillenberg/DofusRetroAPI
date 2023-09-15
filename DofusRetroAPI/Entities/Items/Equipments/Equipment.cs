using DofusRetroAPI.Entities.Items.Equipments.Categories;
using DofusRetroAPI.Entities.Items.Equipments.Sets;

namespace DofusRetroAPI.Entities.Items.Equipments;

public class Equipment : Item
{
    // Equipment Category : Hat, Cloak, Boots,etc...
    public EquipmentCategory EquipmentCategory { get; set; }
    public int EquipmentCategoryId { get; set; }

    // The constraints to equip the item
    public List<EquipmentCondition>? EquipmentConditions { get; set; } = null;
    
    // The effects of the item
    public List<EquipmentEffect> EquipmentEffects { get; set; } = new();
    
    // If the item is part of a set
    public Set? Set { get; set; }
    public int? SetId { get; set; }
}