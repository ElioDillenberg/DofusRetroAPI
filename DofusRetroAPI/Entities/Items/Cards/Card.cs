using DofusRetroAPI.Entities.Items.Cards.Families;

namespace DofusRetroAPI.Entities.Items.Cards;

public sealed class Card : Item
{
    // Card Number
    public int CardNumber { get; set; }
    
    // Rarity 
    public CardRarity CardRarity { get; set; }
    
    // Card FamilyType
    public CardFamily CardFamily { get; set; }
}