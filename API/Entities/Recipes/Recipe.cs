using DofusRetroAPI.Entities.Items;

namespace DofusRetroAPI.Entities.Recipes;

public sealed class Recipe
{
    // PK
    public int Id { get; set; }

    // Item this recipe is for
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    // List of ingredients for this recipe -> ingredient == item + quantity
    public List<Ingredient> Ingredients { get; set; } = new();
}