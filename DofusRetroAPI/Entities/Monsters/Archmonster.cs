namespace DofusRetroAPI.Entities.Monsters;

public class Archmonster : BaseMonster
{
    public Monster Monster { get; set; } = null!;
    public int MonsterId { get; set; }
}