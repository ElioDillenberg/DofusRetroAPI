namespace DofusRetroAPI.Entities.Monsters;

public sealed class ArchMonster : BaseMonster
{
    public ArchMonster(Monster monster)
    {
        Monster = monster;
    }

    public Monster Monster { get; set; }
    public int MonsterId { get; set; }
}