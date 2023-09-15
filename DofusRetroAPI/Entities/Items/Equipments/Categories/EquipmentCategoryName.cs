using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Equipments.Categories;

public sealed class EquipmentCategoryName : BaseLocalizedName
{
    // Reference to the category
    public EquipmentCategory EquipmentCategory { get; set; }
    public int EquipmentCategoryId { get; set; }
}