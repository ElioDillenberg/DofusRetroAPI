using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Equipments.Sets;

public sealed class SetName : BaseLocalizedName
{
    public SetName(Set set)
    {
        Set = set;
    }

    // Reference to the Set
    public Set Set { get; set; }
    public int SetId { get; set; }
}