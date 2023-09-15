using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Monsters.SubAreas;

public sealed class SubAreaName : BaseLocalizedName
{
    public SubArea SubArea { get; set; }
    public int SubAreaId { get; set; }
}