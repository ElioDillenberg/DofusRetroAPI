namespace DofusRetroAPI.Entities.Items.Consumables.ConcreteEntities;

public sealed class UsableItem : Consumable
{
    public override ConsumableType ConsumableType => ConsumableType.UsableItem;
}