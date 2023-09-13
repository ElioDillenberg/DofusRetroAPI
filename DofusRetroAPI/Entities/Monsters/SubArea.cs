namespace DofusRetroAPI.Entities.Monsters;

public class SubArea
{
    // Database Id
    public int Id { get; set; }
    
    // Name of the SubArea
    public string Name { get; set; } = string.Empty;
    
    // Monsters that are a part of this SubArea
    public List<Monster> Monsters { get; set; } = null!;
}