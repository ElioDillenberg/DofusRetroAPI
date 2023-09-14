namespace DofusRetroAPI.Entities.Monsters;

public sealed class Monster : BaseMonster
{
    public ArchMonster? ArchMonster { get; set; } = null!;
    public int? ArchmonsterId { get; set; }
    
    // SubArea the monster is a part of
    public List<SubArea> SubArea { get; set; } = null!;
}