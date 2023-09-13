namespace DofusRetroAPI.Entities.Items;

public class WeaponCharacteristics
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }
    
    // PA
    public int ActionPoints { get; set; }
    
    // CC
    public int CriticalStrikeBonus { get; set; }
    public int CriticalStrikeBaseChance { get; set; }
    
    // Rang
    public int MinRange { get; set; }
    public int MaxRange { get; set; }
    
    public bool OneHand { get; set; }
}