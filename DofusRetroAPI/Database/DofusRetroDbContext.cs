using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Items.Cards;
using DofusRetroAPI.Entities.Items.Consumables;
using DofusRetroAPI.Entities.Items.Equipments;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Pets;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Pets.ResourceEaters;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Pets.SoulEaters;
using DofusRetroAPI.Entities.Items.Equipments.Gear;
using DofusRetroAPI.Entities.Items.Equipments.Sets;
using DofusRetroAPI.Entities.Items.Equipments.Weapons;
using DofusRetroAPI.Entities.Items.Recipes;
using DofusRetroAPI.Entities.Items.Resources;
using DofusRetroAPI.Entities.Localization;
using DofusRetroAPI.Entities.Monsters;
using DofusRetroAPI.Entities.Monsters.Breeds;
using DofusRetroAPI.Entities.Monsters.Ecosystems;
using DofusRetroAPI.Entities.Monsters.SubAreas;
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
        
        // TPC Configuration for Monsters
        modelBuilder.Entity<BaseMonster>().UseTpcMappingStrategy();
        
        // TPC Configuration for BaseLocalizedName
        modelBuilder.Entity<BaseLocalizedName>().UseTpcMappingStrategy();
        
        base.OnModelCreating(modelBuilder);
        
    }

    // Items
    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemName> ItemNames => Set<ItemName>();
    public DbSet<ItemDescription> ItemDescriptions => Set<ItemDescription>();

    // Recipes
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    
    // Consumables
    public DbSet<Consumable> Consumables => Set<Consumable>();

    // Equipments
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<EquipmentEffect> EquipmentEffects => Set<EquipmentEffect>();
    public DbSet<EquipmentCondition> EquipmentConditions => Set<EquipmentCondition>();
    
    // Weapons (subset of Equipment) Daggers, Swords, Axes, Hammers, Shovels, Bows, Wands, Staffs, Daggers, Scythes, Tools, SoulStones, CapturingNets, MagicWeapons
    public DbSet<Weapon> Weapons => Set<Weapon>();

    // Gear (subset of Equipment) Hats, Cloaks, Dofus, Belts, Boots, Rings, Amulets, Shields
    public DbSet<Gear> Gears => Set<Gear>();
    
    // Pets (subset of Equipment) Pets, Dragoturkeys
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<SoulEater> SoulEaters => Set<SoulEater>();
    public DbSet<ResourceEater> FoodEaters => Set<ResourceEater>();
    public DbSet<SoulEaterFood> SoulEaterFoods => Set<SoulEaterFood>();
    public DbSet<ResourceEaterFood> ResourceEaterFoods => Set<ResourceEaterFood>();
    public DbSet<PetFood> PetFoods => Set<PetFood>();
    public DbSet<PetEffect> PetEffects => Set<PetEffect>();
    
    // Sets
    public DbSet<SetName> SetNames => Set<SetName>();
    public DbSet<SetBonus> SetBonuses => Set<SetBonus>();
    public DbSet<SetEffect> SetEffects => Set<SetEffect>     ();
    
    // Resources
    public DbSet<Resource> BaseResources => Set<Resource>();

    // Cards
    public DbSet<Card> Cards => Set<Card>();
    
    // Drops
    public DbSet<Drop> Drops => Set<Drop>();
    
    // Monsters
    public DbSet<Monster> Monsters => Set<Monster>();
    public DbSet<ArchMonster> ArchMonsters => Set<ArchMonster>();
    public DbSet<MonsterName> MonsterNames => Set<MonsterName>();
}