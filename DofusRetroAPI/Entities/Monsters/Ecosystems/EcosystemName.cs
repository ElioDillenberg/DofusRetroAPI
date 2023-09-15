namespace DofusRetroAPI.Entities.Monsters.Ecosystems;

public class EcosystemName
{
    public EcosystemName(Ecosystem ecosystem)
    {
        Ecosystem = ecosystem;
    }

    public Ecosystem Ecosystem { get; set; }
    public int EcosystemId { get; set; }
}