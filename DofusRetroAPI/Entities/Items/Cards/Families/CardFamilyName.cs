using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Cards.Families;

public sealed class CardFamilyName : BaseLocalizedName
{
    public CardFamily CardFamily { get; set; } = null!;
    public int CardFamilyId { get; set; }
}