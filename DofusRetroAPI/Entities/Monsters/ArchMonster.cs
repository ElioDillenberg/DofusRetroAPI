namespace DofusRetroAPI.Entities.Monsters;

public class ArchMonster : BaseMonster
{
    public Monster Monster { get; set; } = null!;
    public int MonsterId { get; set; }
}