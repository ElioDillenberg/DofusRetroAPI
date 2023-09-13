using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public sealed class Consumable : Item
{
    public ConsumableType Type { get; set; }
}