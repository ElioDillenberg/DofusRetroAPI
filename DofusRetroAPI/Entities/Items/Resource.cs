using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public class Resource : Item
{
    public ResourceType Type { get; set; }
}