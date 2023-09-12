using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public class Equipment : Item
{
    public EquipmentType Type { get; set; }
    
    public List<string> Conditions { get; set; }
}