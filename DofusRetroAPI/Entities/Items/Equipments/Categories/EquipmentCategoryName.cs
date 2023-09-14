using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items.Equipments.Categories;

public class EquipmentCategoryName
{
    // Database Id
    public int Id { get; set; }

    // Reference to the category
    public EquipmentCategory EquipmentCategory { get; set; } = null!;
    public int EquipmentCategoryId { get; set; }

    // Language
    public Language Language { get; set; }

    // Localized name
    public string Name { get; set; } = string.Empty;
}