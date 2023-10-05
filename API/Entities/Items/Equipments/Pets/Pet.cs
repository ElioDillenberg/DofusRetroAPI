namespace DofusRetroAPI.Entities.Items.Equipments.Pets;

public class Pet : Equipment
{
    public List<PetEffect> Effects { get; set; } = new();
    
    public bool SoulEater { get; set; }

    public List<ResourceEaterFood>? ResourceFoodTable { get; set; }
    
    public List<SoulEaterFood>? MonsterFoodTable { get; set; }
}