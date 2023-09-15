namespace DofusRetroAPI.Entities.Monsters.Ecosystems;

public class Ecosystem
{
    public int Id { get; set; }

    public List<EcosystemName> EcosystemNames { get; set; } = new();

    public List<BaseMonster> Monsters = new();
}