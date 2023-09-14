using System.ComponentModel.DataAnnotations;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Monsters;

namespace DofusRetroAPI.Entities.Drops;

public class Drop
{
    // Database Id
    public int Id { get; set; }
    
    public int DropTableId { get; set; }
    
    public Monster Monster { get; set; } = null!; 
    public int MonsterId { get; set; }
    
    public Item Item { get; set; } = null!;
    public int ItemId { get; set; }

    [Range(0, 100)]
    public int Rate { get; set; }
    
    public int? DropCap { get; set; }
    
    [Range(0, 800)]
    public int? ProspectionThreshold { get; set; }
}