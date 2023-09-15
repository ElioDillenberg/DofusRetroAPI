using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Items.Cards;
using DofusRetroAPI.Entities.Items.Cards.Families;
using DofusRetroAPI.Entities.Items.Consumables;
using DofusRetroAPI.Entities.Items.Consumables.Categories;
using DofusRetroAPI.Entities.Items.Equipments;
using DofusRetroAPI.Entities.Items.Equipments.Categories;
using DofusRetroAPI.Entities.Items.Equipments.Pets;
using DofusRetroAPI.Entities.Items.Equipments.Pets.FixedStatsPet;
using DofusRetroAPI.Entities.Items.Equipments.Pets.OrnamentalPets;
using DofusRetroAPI.Entities.Items.Equipments.Pets.ResourceEaters;
using DofusRetroAPI.Entities.Items.Equipments.Pets.SoulEaters;
using DofusRetroAPI.Entities.Items.Equipments.Sets;
using DofusRetroAPI.Entities.Items.Equipments.Weapons;
using DofusRetroAPI.Entities.Items.Recipes;
using DofusRetroAPI.Entities.Items.Resources;
using DofusRetroAPI.Entities.Items.Resources.Categories;
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
        
        // modelBuilder.Entity<Blog>()
        //     .HasMany(e => e.Posts)
        //     .WithOne(e => e.Blog)
        //     .HasForeignKey(e => e.BlogId)
        //     .HasPrincipalKey(e => e.Id);
        
        // TPC Configuration for 

        // Configure Table for PetFood
        // modelBuilder.Entity<PetFood>().ToTable("PetFood");

        // Configure Discriminator for pet type foods
        // modelBuilder.Entity<PetFood>()
        //     .HasDiscriminator<string>("FoodType")
        //     .HasValue<SoulEaterFood>("SoulEaterFood")
        //     .HasValue<ResourceEaterFood>("ResourceEaterFood");

        // Add configurations for properties unique to specific pet food types
        // modelBuilder.Entity<PetEffect>().ToTable("PetEffects");

        // TPC Configuration for Item
        
        // modelBuilder.Entity<Card>().ToTable("Cards");
        // modelBuilder.Entity<Resource>().ToTable("Resources");
        // modelBuilder.Entity<Consumable>().ToTable("Consumables");
        // modelBuilder.Entity<Equipment>().ToTable("Equipments");
        
        
        // modelBuilder.Entity<Monster>().ToTable("Monsters");
        // modelBuilder.Entity<ArchMonster>().ToTable("ArchMonsters");

        // modelBuilder.Entity<Item>()
        //     .HasOne(item => item.Recipe)
        //     .WithOne(recipe => recipe.Item)
        //     .HasForeignKey<Recipe>(recipe => recipe.ItemId); 
        
        
        base.OnModelCreating(modelBuilder);
        
    }

    // Db Sets
    public DbSet<ItemName> ItemNames => Set<ItemName>();
    public DbSet<ItemDescription> ItemDescriptions => Set<ItemDescription>();
    
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    
    public DbSet<Consumable> Consumables => Set<Consumable>();
    public DbSet<ConsumableCategory> ConsumableCategories => Set<ConsumableCategory>();
    public DbSet<ConsumableCategoryName> ConsumableCategoryNames => Set<ConsumableCategoryName>();
    
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<ResourceCategory> ResourceCategories => Set<ResourceCategory>();
    public DbSet<ResourceCategoryName> ResourceCategoryNames => Set<ResourceCategoryName>();
    
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<EquipmentCategory> EquipmentCategories => Set<EquipmentCategory>();
    public DbSet<EquipmentCategoryName> EquipmentCategoryNames => Set<EquipmentCategoryName>();
    public DbSet<SetName> SetNames => Set<SetName>();
    public DbSet<SetBonus> SetBonuses => Set<SetBonus>();
    public DbSet<SetEffect> SetEffects => Set<SetEffect>();

    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<SoulEater> SoulEaters => Set<SoulEater>();
    public DbSet<ResourceEater> FoodEaters => Set<ResourceEater>();
    public DbSet<SoulEaterFood> SoulEaterFoods => Set<SoulEaterFood>();
    public DbSet<ResourceEaterFood> ResourceEaterFoods => Set<ResourceEaterFood>();
    public DbSet<OrnamentalPet> OrnamentalPets => Set<OrnamentalPet>();
    public DbSet<FixedStatsPet> FixedStatsPets => Set<FixedStatsPet>();

    public DbSet<PetFood> PetFoods => Set<PetFood>();
    public DbSet<PetEffect> PetEffects => Set<PetEffect>(); 

    public DbSet<Weapon> Weapons => Set<Weapon>();
    
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<CardFamily> CardFamilies => Set<CardFamily>();
    public DbSet<CardFamilyName> CardFamilyNames => Set<CardFamilyName>();
    
    public DbSet<Drop> Drops => Set<Drop>();
    
    public DbSet<Monster> Monsters => Set<Monster>();
    public DbSet<ArchMonster> ArchMonsters => Set<ArchMonster>();
    public DbSet<MonsterName> MonsterNames => Set<MonsterName>();
    
    public DbSet<Ecosystem> Ecosystems => Set<Ecosystem>();
    public DbSet<EcosystemName> EcosystemNames => Set<EcosystemName>();
    
    public DbSet<Breed> Breeds => Set<Breed>();
    public DbSet<BreedName> BreedNames => Set<BreedName>();
    
    public DbSet<SubArea> SubAreas => Set<SubArea>();
    public DbSet<SubAreaName> SubAreaNames => Set<SubAreaName>();
}