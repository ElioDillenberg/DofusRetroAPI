namespace DofusRetroAPI.Entities.Items.Cards.Families;

public class CardFamily
{
    public int Id { get; set; }
    
    public List<Card> Cards { get; set; } = null!;
    
    public List<CardFamilyName> Names { get; set; } = null!;
}