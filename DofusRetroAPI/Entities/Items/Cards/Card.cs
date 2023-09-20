using DofusRetroAPI.Entities.Items.Cards.Families;

namespace DofusRetroAPI.Entities.Items.Cards;

public abstract class Card : Item
{
    // Card Number
    public int CardNumber { get; set; }
    
    // Rarity 
    public abstract CardRarity Rarity { get; }
    
    // Card FamilyType
    public CardFamily CardFamily { get; set; }
    public int CardFamilyId { get; set; }
}