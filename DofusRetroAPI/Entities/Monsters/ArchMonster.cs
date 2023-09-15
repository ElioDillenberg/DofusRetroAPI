namespace DofusRetroAPI.Entities.Monsters;

public sealed class ArchMonster : BaseMonster
{
    public int MonsterId { get; set; }
    public Monster Monster { get; set; } = null!;
}