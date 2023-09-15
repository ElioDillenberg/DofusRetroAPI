namespace DofusRetroAPI.Entities.Items.Equipments.Sets;

public class Set
{
    // Database Id
    public int Id { get; set; }
    
    // Items that are part of the set
    public List<Equipment> Equipments { get; set; } = new();
    
    // Localized set names
    public List<SetName> SetNames { get; set; } = new();
    
    // Bonuses of the set
    public List<SetBonus> SetBonuses { get; set; } = new();
}