using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Monsters;

public sealed class MonsterName : BaseLocalizedName
{
    public Monster Monster { get; set; } = null!;
    public int MonsterId { get; set; }
}