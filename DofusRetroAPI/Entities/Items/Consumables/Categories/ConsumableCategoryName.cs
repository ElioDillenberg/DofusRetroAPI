using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items.Consumables.Categories;

public class ConsumableCategoryName
{
    // Database Id
    public int Id { get; set; }
    
    // Reference to the category
    public ConsumableCategory Category { get; set; } = null!;
    public int ConsumableCategoryId { get; set; }
    
    // Language
    public Language Language { get; set; }

    // Localized name
    public string Name { get; set; } = string.Empty;
}