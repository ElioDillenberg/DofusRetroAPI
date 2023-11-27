using DofusRetroAPI.Entities.Items;

namespace DofusRetroAPI.Entities.Recipes;

public sealed class Ingredient
{
    // Database Id
    public int Id { get; set; }

    // Recipe this ingredient is for
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    // Item
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    
    // Quantity of that item needed for that recipe
    public int Quantity { get; set; }
}