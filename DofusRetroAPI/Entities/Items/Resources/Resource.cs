using DofusRetroAPI.Entities.Items.Resources.Categories;

namespace DofusRetroAPI.Entities.Items.Resources;

public sealed class Resource : Item
{
    public ResourceCategory ResourceCategory { get; set; } = null!;
    public int ResourceCategoryId { get; set; }
}