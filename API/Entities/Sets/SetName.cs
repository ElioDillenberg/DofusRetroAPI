using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Sets;

public sealed class SetName : BaseLocalizedName
{
    // Reference to the Set
    public Set Set { get; set; } = null!;
    public int SetId { get; set; }
}