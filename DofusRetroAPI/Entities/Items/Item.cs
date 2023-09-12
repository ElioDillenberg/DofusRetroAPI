using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items;

public class Item
{
    public int Id { get; set; }
    
    public int GameId { get; set; }
    
    public Dictionary<Language, string> Names { get; set; } = null!;
    
    public int Level { get; set; } = 1;

    public Dictionary<Language, string> Descriptions { get; set; } = null!;

    public int Pods { get; set; } = 0;

    public List<Drop>? DropTable { get; set; } = null!;
    
    public List<Item>? Recipe { get; set; }
}