using DofusRetroAPI.Entities.Monsters.SubAreas;

namespace DofusRetroAPI.Entities.Monsters;

public sealed class Monster : BaseMonster
{
    // Archmonster if it has one
    public ArchMonster? ArchMonster { get; set; }
    
    // SubAreas the monster is a part of
    public List<SubArea> SubAreas { get; set; } = new();
}