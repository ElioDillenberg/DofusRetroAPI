namespace DofusRetroAPI.Entities.Items.Equipments.Pets.SoulEaters;

public class SoulEater : Pet
{
    // List of monsters that the pet can eat
    public List<SoulEaterFood> FoodTable { get; set; } = new();
}