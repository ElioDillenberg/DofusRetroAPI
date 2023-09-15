namespace DofusRetroAPI.Entities.Items.Recipes;

public class Ingredient
{
    // Database Id
    public int Id { get; set; }

    // Recipe this ingredient is for
    public Recipe Recipe { get; set; }
    public int RecipeId { get; set; }

    // Item
    public Item Item { get; set; }
    public int ItemId { get; set; }
    
    // Quantity of that item needed for that recipe
    public int Quantity { get; set; }
}