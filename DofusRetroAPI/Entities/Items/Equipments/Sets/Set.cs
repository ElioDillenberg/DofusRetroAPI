namespace DofusRetroAPI.Entities.Items.Equipments.Sets;

public class Set
{
    // Database Id
    public int Id { get; set; }
    
    // Items that are part of the set
    public List<Item> Items { get; set; } = new();
    
    // Localized set names
    public List<SetName> Names { get; set; } = new();
}