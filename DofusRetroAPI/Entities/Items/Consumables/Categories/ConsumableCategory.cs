namespace DofusRetroAPI.Entities.Items.Consumables.Categories;

public class ConsumableCategory
{
    // Database Id
    public int Id { get; set; }

    // Consumables part of this category
    public List<Consumable> Consumables { get; set; } = new();
    
    // Localized names for this category
    public List<ConsumableCategoryName> ConsumableCategoryNames { get; set; } = new();
}