namespace DofusRetroAPI.Entities.Sets;

public class SetBonus
{
    // Database Id
    public int Id { get; set; }

    // Reference to the Set
    public Set Set { get; set; } = null!;
    public int SetId { get; set; }
    
    // Number of items required to activate the bonus
    public int NumberOfItems { get; set; }
    
    // Effects of the bonus
    public List<SetBonusEffect> SetBonusEffects { get; set; } = new();
}