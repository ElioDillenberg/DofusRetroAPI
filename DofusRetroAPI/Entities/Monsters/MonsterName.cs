using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Monsters;

public class MonsterName
{
    public int Id { get; set; }
    
    public Monster Monster { get; set; } = null!;
    public int MonsterId { get; set; }
    
    public Language Language { get; set; }

    public string Name { get; set; } = string.Empty;
}