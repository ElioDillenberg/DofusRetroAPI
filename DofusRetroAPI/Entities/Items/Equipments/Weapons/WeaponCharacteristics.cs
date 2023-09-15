
namespace DofusRetroAPI.Entities.Items.Equipments.Weapons;

public class WeaponCharacteristic
{
    public int Id { get; set; }

    public Weapon Weapon { get; set; }
    public int WeaponId { get; set; }
    
    // PA
    public int ActionPoints { get; set; }
    
    // CC
    public int CriticalStrikeBonus { get; set; }
    public int CriticalStrikeBaseChance { get; set; }
    
    // Rang
    public int MinRange { get; set; }
    public int MaxRange { get; set; }
    
    // One hand weapon?
    public bool OneHand { get; set; }
}