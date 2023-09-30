namespace DofusRetroAPI.Entities.Items.Equipments;

public sealed class EquipmentCondition
{
    public int Id { get; set; }

    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;

    public EquipmentConditionType ConditionType { get; set; }
    
    public int? Min { get; set; }
    
    public int? Max { get; set; }
}