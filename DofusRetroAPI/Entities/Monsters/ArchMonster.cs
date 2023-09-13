namespace DofusRetroAPI.Entities.Monsters;

public sealed class ArchMonster : BaseMonster
{
    public Monster Monster { get; set; } = null!;
    public int MonsterId { get; set; }
}