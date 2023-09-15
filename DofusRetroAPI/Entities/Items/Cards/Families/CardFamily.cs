namespace DofusRetroAPI.Entities.Items.Cards.Families;

public class CardFamily
{
    // Database Id
    public int Id { get; set; }
    
    // List of cards part of this family
    public List<Card> Cards { get; set; } = null!;

    // Localized card family names
    public List<CardFamilyName> Names { get; set; } = null!;
}