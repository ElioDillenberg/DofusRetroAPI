using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Resources.Categories;

public sealed class ResourceCategoryName : BaseLocalizedName
{
    public ResourceCategoryName(ResourceCategory resourceCategory)
    {
        ResourceCategory = resourceCategory;
    }

    // Reference to the category
    public ResourceCategory ResourceCategory { get; set; }
    public int ResourceCategoryId { get; set; }
}