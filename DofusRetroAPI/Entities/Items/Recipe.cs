namespace DofusRetroAPI.Entities.Items;

public class Recipe
{
    public int Id { get; set; }

    public Item Item { get; set; } = null!;
    public int ItemId { get; set; }

    private Dictionary<int, Item> Ingredients { get; set; } = null!;
}