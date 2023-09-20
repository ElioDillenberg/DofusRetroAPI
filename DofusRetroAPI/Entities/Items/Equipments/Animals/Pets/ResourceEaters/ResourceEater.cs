namespace DofusRetroAPI.Entities.Items.Equipments.Animals.Pets.ResourceEaters;

public class ResourceEater : Pet
{
    // List of resources that the pet can eat
    public List<ResourceEaterFood> FoodTable { get; set; } = new();
}