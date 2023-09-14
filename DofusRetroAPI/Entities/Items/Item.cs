using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Items.Recipes;

namespace DofusRetroAPI.Entities.Items;

public abstract class Item
{
    // Database Id
    public int Id { get; set; }
    
    // Id in the game's database
    public int GameId { get; set; }

    // List of localized names
    public List<ItemName> Names { get; set; } = null!;

    // Level of the item
    public int Level { get; set; } = 1;

    // List of localized descriptions
    public List<ItemDescription> Descriptions { get; set; } = null!;

    // Weight in pods
    public int Pods { get; set; } = 0;

    // If the item can be looted from monsters is has a drop table
    public List<Drop>? DropTable { get; set; } = null!;
    
    // If the item can be crafted
    public Recipe? Recipe { get; set; }
    
    // This is the ID of the png image to be used for that specific item
    public int Image { get; set; }
}