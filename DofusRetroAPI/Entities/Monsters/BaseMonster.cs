using DofusRetroAPI.Entities.Monsters.Breeds;
using DofusRetroAPI.Entities.Monsters.Ecosystems;

namespace DofusRetroAPI.Entities.Monsters;

public abstract class BaseMonster 
{
    // Db Id
    public int Id { get; set; }
    
    // Id as it is in the Dofus Retro client
    public int GameId { get; set; }
    
    // Name of the Monster
    public List<MonsterName> Names { get; set; } = new();
    
    // Ecosystem the monster is a part of
    public Ecosystem? Ecosystem { get; set; }

    // Breed the monster is a part of
    public Breed? Breed { get; set; }
    
    // Characteristics of Monster (Level + Characteristics)
    public List<MonsterCharacteristic> Characteristics { get; set; } = new();
}