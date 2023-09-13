namespace DofusRetroAPI.Entities.Items;

public class Set
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public List<Item> Items { get; set; } = null!;
}