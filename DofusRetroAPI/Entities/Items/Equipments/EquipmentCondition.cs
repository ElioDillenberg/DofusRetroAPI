using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items.Equipments;

public class EquipmentCondition
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }

    public EquipmentConditionType ConditionType { get; set; }
 
    public Effect Effect { get; set; } = null!;
}