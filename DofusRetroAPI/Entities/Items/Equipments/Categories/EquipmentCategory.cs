namespace DofusRetroAPI.Entities.Items.Equipments.Categories;

public class EquipmentCategory
{
    // Database Id
    public int Id { get; set; }
    
    // Localized equipment category names
    public List<EquipmentCategoryName> EquipmentCategoryNames { get; set; } = new();

    // Equipments part of this category
    public List<Equipment> Equipments { get; set; } = new();
}