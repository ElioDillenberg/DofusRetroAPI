using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public abstract class Item
{
    public int Id { get; set; }
    
    public int GameId { get; set; }
    
    public Dictionary<Language, string> Names { get; set; } = null!;
    
    public int Level { get; set; } = 1;

    public Dictionary<Language, string> Descriptions { get; set; } = null!;

    // Weight in pods
    public int Pods { get; set; } = 0;

    // If the item can be looted from monsters
    public List<Drop>? DropTable { get; set; } = null!;
    
    // If the item can be crafted
    public List<Item>? Recipe { get; set; }
    
    public int Image { get; set; }
}