namespace DofusRetroAPI.Entities.Monsters;

public class Monster : BaseMonster
{
    public Archmonster Archmonster { get; set; } = null!;
    public int ArchmonsterId { get; set; }
}