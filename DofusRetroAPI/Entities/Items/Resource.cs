using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public sealed class Resource : Item
{
    public ResourceType Type { get; set; }
}