namespace DofusRetroAPI.Entities.Items.Resources;

public abstract class BaseResource : Item
{
    public abstract ResourceType ResourceType { get; }
}