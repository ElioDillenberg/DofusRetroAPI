namespace DofusRetroAPI.Entities.Items.Cards;

public sealed class Card : Item
{
    // Card Number
    public int CardNumber { get; set; }
    
    // Card Family
    public CardFamily CardFamily { get; set; }
}