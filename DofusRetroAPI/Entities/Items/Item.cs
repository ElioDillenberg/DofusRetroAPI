using System.ComponentModel.DataAnnotations;
using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Items.Equipments.Sets;
using DofusRetroAPI.Entities.Items.Recipes;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Entities.Items;

[Index(nameof(Image), IsUnique = true)]
[Index(nameof(GameId), IsUnique = true)]
public abstract class Item
{
    public int Id { get; set; }
    
    // Id in the game's database
    public int GameId { get; set; }

    // List of localized names
    [Required]
    public List<ItemName> Names { get; set; } = new ();

    // Level of the item
    [Required]
    public int Level { get; set; } = 1;

    // List of localized descriptions
    public List<ItemDescription> Descriptions { get; set; } = new();

    // Weight in pods
    [Required]
    public int Pods { get; set; } = 0;

    // If the item can be looted from monsters is has a drop table
    public List<Drop> DropTable { get; set; } = new();
    
    // If the item can be crafted
    public Recipe? Recipe { get; set; }
    
    // This is the ID of the png image to be used for that specific item
    [Required]
    public int Image { get; set; }
}