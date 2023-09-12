namespace DofusRetroAPI.Entities.Monsters;

public class Monster : BaseMonster
{
    public ArchMonster? ArchMonster { get; set; } = null!;
    public int? ArchmonsterId { get; set; }
}