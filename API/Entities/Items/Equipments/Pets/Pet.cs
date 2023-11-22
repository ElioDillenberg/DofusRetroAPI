namespace DofusRetroAPI.Entities.Items.Equipments.Pets;

public class Pet : Equipment
{
    // TODO WIP
    public List<PetEffect> PetEffects { get; set; } = new();
    
    public bool SoulEater { get; set; }

    public List<ResourceEaterFood>? ResourceFoodTable { get; set; }
    
    public List<SoulEaterFood>? MonsterFoodTable { get; set; }
}