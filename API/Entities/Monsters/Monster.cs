using System.ComponentModel.DataAnnotations;
using DofusRetroAPI.Entities.Monsters.Breeds;
using DofusRetroAPI.Entities.Monsters.Ecosystems;

namespace DofusRetroAPI.Entities.Monsters;

// [Index(nameof(GameId), IsUnique = true)]
public class Monster
{
    // PK DB id (same as DofusRetroClient)
    public int Id { get; set; }
    
    // Name of the Monster
    public List<MonsterName> MonsterNames { get; set; } = new();
    
    // Ecosystem the monster is a part of
    public Ecosystem Ecosystem { get; set; }

    // Breed the monster is a part of
    public Breed Breed { get; set; }
    
    // Characteristics of Monster (Level + Characteristics)
    public List<MonsterCharacteristic> Characteristics { get; set; } = new();
    
    // In case of normal monster, this is the archmonster,
    // In case of archmonster, this is the normal monster
    public int? RelatedMonsterId { get; set; }
    public Monster? RelatedMonster { get; set; }
}