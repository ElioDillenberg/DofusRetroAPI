using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Items.Cards;
using DofusRetroAPI.Entities.Items.Cards.Families;
using DofusRetroAPI.Entities.Items.Consumables;
using DofusRetroAPI.Entities.Items.Consumables.Categories;
using DofusRetroAPI.Entities.Items.Equipments;
using DofusRetroAPI.Entities.Items.Equipments.Categories;
using DofusRetroAPI.Entities.Items.Equipments.Sets;
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
        
        // TCP stands for Table Per Concrete Type
        // It means each class will have its own table
        
        // Configure TPC for Equipment
        modelBuilder.Entity<Equipment>().ToTable("Equipments");
        modelBuilder.Entity<Equipment>().HasBaseType<Item>();

        // Configure TPC for Consumable
        modelBuilder.Entity<Consumable>().ToTable("Consumables");
        modelBuilder.Entity<Consumable>().HasBaseType<Item>(); 

        // Configure TPC for Card
        modelBuilder.Entity<Card>().ToTable("Cards");
        modelBuilder.Entity<Card>().HasBaseType<Item>();

        // Configure TPC for Resource
        modelBuilder.Entity<Resource>().ToTable("Resources");
        modelBuilder.Entity<Resource>().HasBaseType<Item>();
        
        // Configure TPC for Monster
        modelBuilder.Entity<Monster>().ToTable("Monsters");
        modelBuilder.Entity<Monster>().HasBaseType<BaseMonster>();
        
        // Configure TPC for ArchMonster
        modelBuilder.Entity<ArchMonster>().ToTable("ArchMonsters");
        modelBuilder.Entity<ArchMonster>().HasBaseType<BaseMonster>();
        
        // Configure TPC for ItemName
        modelBuilder.Entity<ItemName>().ToTable("ItemNames");
        modelBuilder.Entity<ItemName>().HasBaseType<BaseLocalizedName>();
        
        // Configure TPC for ResourceCategoryName
        modelBuilder.Entity<ResourceCategoryName>().ToTable("ResourceCategoryNames");
        modelBuilder.Entity<ResourceCategoryName>().HasBaseType<BaseLocalizedName>();
        
        // Configure TPC for EquipmentCategoryName
        modelBuilder.Entity<EquipmentCategoryName>().ToTable("EquipmentCategoryNames");
        modelBuilder.Entity<EquipmentCategoryName>().HasBaseType<BaseLocalizedName>();
        
        // Configure TPC for ConsumableCategoryName
        modelBuilder.Entity<ConsumableCategoryName>().ToTable("ConsumableCategoryNames");
        modelBuilder.Entity<ConsumableCategoryName>().HasBaseType<BaseLocalizedName>();
        
        // Configure TPC for SetName
        modelBuilder.Entity<SetName>().ToTable("SetNames");
        modelBuilder.Entity<SetName>().HasBaseType<BaseLocalizedName>();
        
        // Configure TPC for CardFamilyName
        modelBuilder.Entity<CardFamilyName>().ToTable("CardFamilyNames");
        modelBuilder.Entity<CardFamilyName>().HasBaseType<BaseLocalizedName>();
        
        // configure TPC for MonsterName
        modelBuilder.Entity<MonsterName>().ToTable("MonsterNames");
        modelBuilder.Entity<MonsterName>().HasBaseType<BaseLocalizedName>();
        
        // Configure TPC for SubAreaName
        modelBuilder.Entity<SubAreaName>().ToTable("SubAreaNames");
        modelBuilder.Entity<SubAreaName>().HasBaseType<BaseLocalizedName>();
        
        // Configure TPC for EcosystemName
        modelBuilder.Entity<EcosystemName>().ToTable("EcosystemNames");
        modelBuilder.Entity<EcosystemName>().HasBaseType<BaseLocalizedName>();
        
        // Configure TPC for BreedName
        modelBuilder.Entity<BreedName>().ToTable("BreedNames");
        modelBuilder.Entity<BreedName>().HasBaseType<BaseLocalizedName>();
        
        base.OnModelCreating(modelBuilder);
    }

    // Db Sets
    public DbSet<Consumable> Consumables => Set<Consumable>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<Card> Cards => Set<Card>();
}