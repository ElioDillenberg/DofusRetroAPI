using DofusRetroAPI.Entities.Items.Cards.Families;

namespace DofusRetroAPI.Entities.Items.Cards;

public class Card : Item
{
    // Card Number
    public int CardNumber { get; set; }
    
    // Rarity 
    public CardRarity Rarity { get; set; }
    
    // Card FamilyType
    public CardFamily CardFamily { get; set; }
    public int CardFamilyId { get; set; }
}