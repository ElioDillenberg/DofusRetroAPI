using DofusRetroAPI.Entities.Monsters.SubAreas;

namespace DofusRetroAPI.Entities.Monsters;

public sealed class Monster : BaseMonster
{
    public ArchMonster? ArchMonster { get; set; }
    public int? ArchmonsterId { get; set; }
    
    // SubAreas the monster is a part of
    public List<SubArea> SubAreas { get; set; } = new();
}