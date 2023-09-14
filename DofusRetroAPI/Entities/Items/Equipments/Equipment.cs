using DofusRetroAPI.Entities.Enums;
using DofusRetroAPI.Entities.Items.Equipments.Sets;

namespace DofusRetroAPI.Entities.Items.Equipments;

public sealed class Equipment : Item
{
    public EquipmentType Type { get; set; }

    // The constraints to equip the item
    public List<EquipmentCondition> Constraints { get; set; } = null!;
    
    // The effects of the item
    public List<Effect> Effects { get; set; } = null!;
    
    // If the item is part of a set
    public Set? Set { get; set; }
    
    // If the item is a weapon, it holds Weapon Characteristics
    public WeaponCharacteristics? WeaponCharacteristics { get; set; } = null!;
}