namespace DofusRetroAPI.Entities.Items.Consumables;

public abstract class Consumable : Item
{
    public abstract ConsumableType ConsumableType { get; }
}