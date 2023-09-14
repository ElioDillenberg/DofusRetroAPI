namespace DofusRetroAPI.Entities.Items.Resources.Categories;

public class ResourceCategory
{
    // Database Id
    public int Id { get; set; }
    
    // Resources part of this category
    public List<Resource> Resources { get; set; } = null!;
    
    // Localized names for this category
    public List<ResourceCategoryName> Names { get; set; } = null!;
}