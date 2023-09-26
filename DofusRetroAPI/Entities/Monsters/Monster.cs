using Microsoft.EntityFrameworkCore;
using DofusRetroAPI.Entities.Monsters.Breeds;
using DofusRetroAPI.Entities.Monsters.Ecosystems;

namespace DofusRetroAPI.Entities.Monsters;

[Index(nameof(GameId), IsUnique = true)]
public abstract class Monster
{
    // Db Id
    public int Id { get; set; }
    
    // Id as it is in the Dofus Retro client
    public int GameId { get; set; }
    
    // Name of the Monster
    public List<MonsterName> MonsterNames { get; set; } = new();
    
    // Ecosystem the monster is a part of
    public Ecosystem Ecosystem { get; set; }

    // Breed the monster is a part of
    public Breed Breed { get; set; }
    
    // Characteristics of Monster (Level + Characteristics)
    public List<MonsterCharacteristic> Characteristics { get; set; } = new();
    
    // COULD ADD SPELLS HERE, NOT FOR THE MOMENT THO!
}