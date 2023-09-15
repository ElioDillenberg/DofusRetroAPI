using DofusRetroAPI.Entities.Items.Equipments.Categories;
using DofusRetroAPI.Entities.Items.Equipments.Sets;

namespace DofusRetroAPI.Entities.Items.Equipments;

public sealed class Equipment : Item
{
    // Equipment Category : Hat, Cloak, Boots,etc...
    public EquipmentCategory EquipmentCategory { get; set; } = new();

    // The constraints to equip the item : 
    public List<EquipmentCondition>? Constraints { get; set; } = null;
    
    // The effects of the item
    public List<Effect> Effects { get; set; } = new();
    
    // If the item is part of a set
    public Set? Set { get; set; } = null;
    
    // If the item is a weapon, it holds Weapon Characteristics
    public WeaponCharacteristics? WeaponCharacteristics { get; set; }
}