using System.ComponentModel.DataAnnotations;
using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Items.Conditions;
using DofusRetroAPI.Entities.Items.Effects;
using DofusRetroAPI.Entities.Items.Recipes;
using DofusRetroAPI.Entities.Items.Resources;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Entities.Items;

[Index(nameof(Id), IsUnique = true)]
public abstract class Item
{
    // PK DB id (same as DofusRetroClient)
    public int Id { get; set; }

    // List of localized names
    [Required]
    public List<ItemName> Names { get; set; } = new ();

    // Level of the item
    [Required]
    public int Level { get; set; } = 1;

    // Type of the item
    public ItemType ItemType { get; set; }

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
    public int? Image { get; set; }
    
    // This is the vendor price of the item
    [Required]
    public int Price { get; set; }
    
    // This string represents the conditions needed to use/equip the item
    public List<ItemCondition> Conditions { get; set; } = new();
    
    // This string represents the effects of the item
    public List<ItemStat> Stats { get; set; } = new();
}