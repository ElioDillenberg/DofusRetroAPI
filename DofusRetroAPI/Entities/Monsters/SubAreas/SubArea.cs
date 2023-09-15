namespace DofusRetroAPI.Entities.Monsters.SubAreas;

public class SubArea
{
    // Database Id
    public int Id { get; set; }
    
    // Name of the SubArea
    public List<SubAreaName> SubAreaNames { get; set; } = new();
    
    // Monsters that are a part of this SubArea
    public List<Monster> Monsters { get; set; } = new();
}