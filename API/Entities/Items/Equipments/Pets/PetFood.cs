namespace DofusRetroAPI.Entities.Items.Equipments.Pets;

public abstract class PetFood
{
    // TODO WIP
    public int Id { get; set; }

    public PetEffect PetEffect { get; set; } = null!;
    public int PetEffectId { get; set; }
    
    public int EffectIncrease { get; set; }
}