namespace DofusRetroAPI.Entities.Items.Equipments.Weapons;

public class Weapon : Equipment
{
    // Weapon specific characteristic
    public WeaponCharacteristic? WeaponCharacteristic { get; set; }
}