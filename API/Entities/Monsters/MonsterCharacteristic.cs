namespace DofusRetroAPI.Entities.Monsters;

public sealed class MonsterCharacteristic
{
    public int Id { get; set; }

    public int Rank { get; set; }

    public Monster Monster { get; set; } = null!;
    public int MonsterId { get; set; }

    public int Level { get; set; }

    public int HealthPoints { get; set; }

    public int ActionPoints { get; set; }

    public int MovementPoints { get; set; }

    public int Experience { get; set; }

    public int Initiative { get; set; }

    public int Strength { get; set; }

    public int Chance { get; set; }

    public int Intelligence { get; set; }

    public int Agility { get; set; }

    public int ActionPointAvoidance { get; set; }

    public int MovementPointAvoidance { get; set; }

    public int NeutralResistancePercentage { get; set; }

    public int EarthResistancePercentage { get; set; }

    public int FireResistancePercentage { get; set; }

    public int WaterResistancePercentage { get; set; }

    public int AirResistancePercentage { get; set; }
}