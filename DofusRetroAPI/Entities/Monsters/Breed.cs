namespace DofusRetroAPI.Entities.Monsters;

public class Breed
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public List<BaseMonster> Monsters = null!;
}