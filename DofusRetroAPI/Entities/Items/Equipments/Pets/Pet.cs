namespace DofusRetroAPI.Entities.Items.Equipments.Pets;

public abstract class Pet : Equipment
{
    public override EquipmentType EquipmentType => EquipmentType.Pet;

    public List<PetEffect> Effects { get; set; } = new();
}