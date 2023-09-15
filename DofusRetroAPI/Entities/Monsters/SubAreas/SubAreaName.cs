using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Monsters.SubAreas;

public sealed class SubAreaName : BaseLocalizedName
{
    public SubAreaName(SubArea subArea)
    {
        SubArea = subArea;
    }

    public SubArea SubArea { get; set; }
    public int SubAreaId { get; set; }
}