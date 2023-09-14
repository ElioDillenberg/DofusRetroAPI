using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items.Cards.Families;

public class CardFamilyName
{
    // Database Id
    public int Id { get; set; }
    
    public CardFamily CardFamily { get; set; } = null!;
    public int CardFamilyId { get; set; }
    
    // Language of the name
    public Language Language { get; set; }
    
    // Localized name
    public string Name { get; set; } = string.Empty;
}