namespace DofusRetroAPI.Entities.Items.Equipments.Pets;

public abstract class PetFood
{
    public int Id { get; set; }

    public EquipmentEffect Effect { get; set; } = null!;
    public int EquipmentEffectId { get; set; }
    
    public int EffectIncrease { get; set; }
}