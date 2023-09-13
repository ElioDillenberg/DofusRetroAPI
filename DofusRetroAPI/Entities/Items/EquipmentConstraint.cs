using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public class EquipmentConstraint
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }

    public EquipmentConstraintType ConstraintType { get; set; }
 
    public Effect Effect { get; set; } = null!;
}