namespace DofusRetroAPI.Entities.Items.Recipes;

public sealed class Recipe
{
    // Database Id
    public int Id { get; set; }

    // Item this recipe is for
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    
    // Is this recipe secret?
    public bool IsSecret { get; set; }

    // List of ingredients for this recipe -> ingredient == item + quantity
    private List<Ingredient> Ingredients { get; set; } = new();
}