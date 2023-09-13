using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public sealed class Equipment : Item
{
    public EquipmentType Type { get; set; }

    // The constraints to equip the item
    public List<string> Constraints { get; set; } = null!;
    
    // The effects of the item
    public List<Effect> Effects { get; set; } = null!;
    
    // If the item is part of a set
    public Set? Set { get; set; }
    
    // If the item
    public WeaponCharacteristics? WeaponCharacteristics { get; set; } = null!;
}