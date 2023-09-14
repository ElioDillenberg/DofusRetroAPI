namespace DofusRetroAPI.Entities.Monsters;

public class Ecosystem
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public List<BaseMonster> Monsters = null!;
}