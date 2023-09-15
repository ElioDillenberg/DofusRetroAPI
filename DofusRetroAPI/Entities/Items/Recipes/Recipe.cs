namespace DofusRetroAPI.Entities.Items.Recipes;

public sealed class Recipe
{
    // Constructor
    public Recipe(Item item)
    {
        Item = item;
    }
    
    // Database Id
    public int Id { get; set; }

    // Item this recipe is for
    public Item Item { get; set; }
    public int ItemId { get; set; }
    
    // Is this recipe secret?
    public bool IsSecret { get; set; }

    // List of ingredients for this recipe -> ingredient == item + quantity
    private List<Ingredient> Ingredients { get; set; } = new();
}