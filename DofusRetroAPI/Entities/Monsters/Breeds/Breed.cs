namespace DofusRetroAPI.Entities.Monsters.Breeds;

public class Breed
{
    public int Id { get; set; }

    public List<BreedName> BreedNames { get; set; } = new();  
    
    public List<BaseMonster> Monsters = new();
}