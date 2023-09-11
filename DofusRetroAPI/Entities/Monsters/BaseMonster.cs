namespace DofusRetroAPI.Entities.Monsters;

public abstract class BaseMonster 
{
    public int Id { get; set; }
    
    public int GameId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public Ecosystem Ecosystem { get; set; } = null!;
    public int EcosystemId { get; set; }
    
    public Breed Breed { get; set; } = null!;
    public int BreedId { get; set; }
}