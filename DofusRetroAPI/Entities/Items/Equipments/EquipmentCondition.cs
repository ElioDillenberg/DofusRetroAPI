namespace DofusRetroAPI.Entities.Items.Equipments;

public sealed class EquipmentCondition
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }

    public EquipmentConditionType ConditionType { get; set; }
    
    public int? Min { get; set; }
    
    public int? Max { get; set; }
}