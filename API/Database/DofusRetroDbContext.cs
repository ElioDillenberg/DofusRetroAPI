using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Items.Cards;
using DofusRetroAPI.Entities.Items.Conditions;
using DofusRetroAPI.Entities.Items.Consumables;
using DofusRetroAPI.Entities.Items.Effects;
using DofusRetroAPI.Entities.Items.Equipments;
using DofusRetroAPI.Entities.Items.Equipments.Gear;
using DofusRetroAPI.Entities.Items.Equipments.Pets;
using DofusRetroAPI.Entities.Items.Equipments.Weapons;
using DofusRetroAPI.Entities.Items.Recipes;
using DofusRetroAPI.Entities.Items.Resources;
using DofusRetroAPI.Entities.Localization;
using DofusRetroAPI.Entities.Monsters;
using DofusRetroAPI.Entities.Sets;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Database;

public class DofusRetroDbContext : DbContext
{
    public DofusRetroDbContext (DbContextOptions<DofusRetroDbContext> options) : base(options) {}
    
    // Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPC Configuration for Items
        modelBuilder.Entity<Item>().UseTpcMappingStrategy();
        
        // TPC Configuration for BaseLocalizedName
        modelBuilder.Entity<BaseLocalizedText>().UseTpcMappingStrategy();
        
        modelBuilder.Entity<Monster>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Item>()
            .Property(e => e.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Set>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
        
        base.OnModelCreating(modelBuilder);
    }

    // Localization
    public DbSet<BaseLocalizedText> LocalizedTexts => Set<BaseLocalizedText>();
    
    // Items
    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemName> ItemNames => Set<ItemName>();
    public DbSet<ItemDescription> ItemDescriptions => Set<ItemDescription>();
    
    public DbSet<ItemCondition> ItemConditions => Set<ItemCondition>();
    public DbSet<ItemConditionText> ItemConditionTexts => Set<ItemConditionText>();
    
    public DbSet<ItemStat> ItemStats => Set<ItemStat>();

    // Recipes
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    
    // Consumables
    public DbSet<Consumable> Consumables => Set<Consumable>();

    // Equipments
    public DbSet<Equipment> Equipments => Set<Equipment>();
    
    
    // Weapons (subset of Equipment) Daggers, Swords, Axes, Hammers, Shovels, Bows, Wands, Staffs, Daggers, Scythes, Tools, SoulStones, CapturingNets, MagicWeapons
    public DbSet<Weapon> Weapons => Set<Weapon>();

    // Gear (subset of Equipment) Hats, Cloaks, Dofus, Belts, Boots, Rings, Amulets, Shields
    public DbSet<Gear> Gears => Set<Gear>();
    
    // Pets (subset of Equipment) Pets, Dragoturkeys
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<SoulEaterFood> SoulEaterFoods => Set<SoulEaterFood>();
    public DbSet<ResourceEaterFood> ResourceEaterFoods => Set<ResourceEaterFood>();
    public DbSet<PetFood> PetFoods => Set<PetFood>();
    public DbSet<PetEffect> PetEffects => Set<PetEffect>();
    
    // Sets
    public DbSet<Set> Sets => Set<Set>();
    public DbSet<SetName> SetNames => Set<SetName>();
    public DbSet<SetBonus> SetBonuses => Set<SetBonus>();
    
    // Resources
    public DbSet<Resource> Resources => Set<Resource>();

    // Cards
    public DbSet<Card> Cards => Set<Card>();
    
    // Drops
    public DbSet<Drop> Drops => Set<Drop>();
    
    // Monsters
    public DbSet<Monster> Monsters => Set<Monster>();
    public DbSet<MonsterName> MonsterNames => Set<MonsterName>();
    public DbSet<MonsterCharacteristic> MonsterCharacteristics => Set<MonsterCharacteristic>();
}