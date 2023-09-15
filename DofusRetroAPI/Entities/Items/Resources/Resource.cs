using DofusRetroAPI.Entities.Items.Resources.Categories;

namespace DofusRetroAPI.Entities.Items.Resources;

public sealed class Resource : Item
{
    public ResourceCategory ResourceCategory { get; set; } = new();
    public int ResourceCategoryId { get; set; }
}