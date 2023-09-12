using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public class Consumable : Item
{
    public ConsumableType Type { get; set; }
}