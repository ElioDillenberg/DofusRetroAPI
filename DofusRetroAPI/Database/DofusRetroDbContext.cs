using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Items.Cards;
using DofusRetroAPI.Entities.Items.Cards.Families;
using DofusRetroAPI.Entities.Items.Consumables;
using DofusRetroAPI.Entities.Items.Consumables.ConcreteEntities;
using DofusRetroAPI.Entities.Items.Consumables.Food;
using DofusRetroAPI.Entities.Items.Equipments;
using DofusRetroAPI.Entities.Items.Equipments.Animals;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Dragoturkey;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Pets;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Pets.FixedStatsPet;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Pets.OrnamentalPets;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Pets.ResourceEaters;
using DofusRetroAPI.Entities.Items.Equipments.Animals.Pets.SoulEaters;
using DofusRetroAPI.Entities.Items.Equipments.Gear;
using DofusRetroAPI.Entities.Items.Equipments.Other;
using DofusRetroAPI.Entities.Items.Equipments.Sets;
using DofusRetroAPI.Entities.Items.Equipments.Weapons;
using DofusRetroAPI.Entities.Items.Equipments.Weapons.WeaponEntities;
using DofusRetroAPI.Entities.Items.Recipes;
using DofusRetroAPI.Entities.Items.Resources;
using DofusRetroAPI.Entities.Items.Resources.ResourceEntities;
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

    // Db Sets
    
    // Items
    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemName> ItemNames => Set<ItemName>();
    public DbSet<ItemDescription> ItemDescriptions => Set<ItemDescription>();
    
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    
    // Abstract Consumables
    public DbSet<Consumable> Consumables => Set<Consumable>();

    // Concrete Consumables
    public DbSet<Potion> Potions => Set<Potion>();
    public DbSet<PetPotion> PetPotions => Set<PetPotion>();
    public DbSet<Dye> Dyes => Set<Dye>();
    public DbSet<ForgetfulnessPotion> ForgetfulnessPotions => Set<ForgetfulnessPotion>();
    public DbSet<ForgetfulnessPotionForCollectors> ForgetfulnessPotionsForCollectors => Set<ForgetfulnessPotionForCollectors>();
    public DbSet<SkillLossPotion> SkillLossPotions => Set<SkillLossPotion>();
    public DbSet<UnlearningPotionForProfession> UnlearningPotionsForProfessions => Set<UnlearningPotionForProfession>();
    public DbSet<Beer> Beers => Set<Beer>();
    public DbSet<Bread> Breads => Set<Bread>();
    public DbSet<Candy> Candies => Set<Candy>();
    public DbSet<Drink> Drinks => Set<Drink>();
    public DbSet<EdibleFish> EdibleFishes => Set<EdibleFish>();
    public DbSet<EdibleMeat> EdibleMeats => Set<EdibleMeat>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<ExperienceScroll> ExperienceScrolls => Set<ExperienceScroll>();
    public DbSet<CharacteristicScroll> CharacteristicScrolls => Set<CharacteristicScroll>();
    public DbSet<SpellScroll> SpellScrolls => Set<SpellScroll>();
    public DbSet<MountCertificate> MountCertificates => Set<MountCertificate>();
    public DbSet<BowKennelCertificate> BowKennelCertificates => Set<BowKennelCertificate>();
    public DbSet<SeekerScroll> SeekerScrolls => Set<SeekerScroll>();
    public DbSet<SpiritGem> SpiritGems => Set<SpiritGem>();
    public DbSet<BagOfResources> BagOfResources => Set<BagOfResources>();
    public DbSet<PetEgg> PetEggs => Set<PetEgg>();
    public DbSet<FairyWork> FairyWorks => Set<FairyWork>();
    public DbSet<MagicStone> MagicStones => Set<MagicStone>();
    public DbSet<Gift> Gifts => Set<Gift>();
    public DbSet<BreedingItem> BreedingItems => Set<BreedingItem>();
    public DbSet<UsableItem> UsableItems => Set<UsableItem>();
    public DbSet<QuestItem> QuestItems => Set<QuestItem>();
    public DbSet<Prism> Prisms => Set<Prism>();
    public DbSet<FragmentOfShushuSoul> FragmentOfShushuSouls => Set<FragmentOfShushuSoul>();

    // Equipments
    public DbSet<Equipment> Equipment => Set<Equipment>();
    
    // Gear
    public DbSet<Hat> Gears => Set<Hat>();
    public DbSet<Amulet> Amulets => Set<Amulet>();
    public DbSet<Boots> Boots => Set<Boots>();
    public DbSet<Cloak> Capes => Set<Cloak>();
    public DbSet<Belt> Belts => Set<Belt>();
    public DbSet<Ring> Rings => Set<Ring>();
    public DbSet<Shield> Shields => Set<Shield>();
    public DbSet<Dofus> Dofus => Set<Dofus>();
    public DbSet<Backpack> Backpacks => Set<Backpack>();
    
    // Weapons
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<Axe> Axes => Set<Axe>();
    public DbSet<Bow> Bows => Set<Bow>();
    public DbSet<Daggers> Daggers => Set<Daggers>();
    public DbSet<Hammer> Hammers => Set<Hammer>();
    public DbSet<Shovel> Shovels => Set<Shovel>();
    public DbSet<Sword> Swords => Set<Sword>();
    public DbSet<Wand> Wands => Set<Wand>();
    public DbSet<Staff> Staves => Set<Staff>();
    public DbSet<Pickaxe> Pickaxes => Set<Pickaxe>();
    public DbSet<Scythe> Scythes => Set<Scythe>();
    public DbSet<MagicWeapon> MagicWeapons => Set<MagicWeapon>();
    public DbSet<Tool> Tools => Set<Tool>();
    
    // Others
    public DbSet<SoulStone> SoulStones => Set<SoulStone>();
    public DbSet<CapturingNet> CapturingNets => Set<CapturingNet>();
    public DbSet<SetName> SetNames => Set<SetName>();
    public DbSet<SetBonus> SetBonuses => Set<SetBonus>();
    public DbSet<SetEffect> SetEffects => Set<SetEffect>();
    
    // Animals
    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<Dragoturkey> Dragoturkeys => Set<Dragoturkey>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<SoulEater> SoulEaters => Set<SoulEater>();
    public DbSet<ResourceEater> FoodEaters => Set<ResourceEater>();
    public DbSet<SoulEaterFood> SoulEaterFoods => Set<SoulEaterFood>();
    public DbSet<ResourceEaterFood> ResourceEaterFoods => Set<ResourceEaterFood>();
    public DbSet<OrnamentalPet> OrnamentalPets => Set<OrnamentalPet>();
    public DbSet<FixedStatsPet> FixedStatsPets => Set<FixedStatsPet>();
    public DbSet<PetFood> PetFoods => Set<PetFood>();
    public DbSet<PetEffect> PetEffects => Set<PetEffect>(); 
    
    // Resources
    public DbSet<BaseResource> BaseResources => Set<BaseResource>();
    public DbSet<AlchemyEquipment> AlchemyEquipments => Set<AlchemyEquipment>();
    public DbSet<Alloy> Alloys => Set<Alloy>();
    public DbSet<Bark> Barks => Set<Bark>();
    public DbSet<Bone> Bones => Set<Bone>();
    public DbSet<Bud> Buds => Set<Bud>();
    public DbSet<Carapace> Carapaces => Set<Carapace>();
    public DbSet<Cereal> Cereals => Set<Cereal>();
    public DbSet<Ear> Ears => Set<Ear>();
    public DbSet<Egg> Eggs => Set<Egg>();
    public DbSet<Eye> Eyes => Set<Eye>();
    public DbSet<Fabric> Fabrics => Set<Fabric>();
    public DbSet<Feather> Feathers => Set<Feather>();
    public DbSet<Fish> Fishes => Set<Fish>();
    public DbSet<Flour> Flours => Set<Flour>();
    public DbSet<Flower> Flowers => Set<Flower>();
    public DbSet<Fruit> Fruits => Set<Fruit>();
    public DbSet<GuttedFish> GuttedFishes => Set<GuttedFish>();
    public DbSet<Hair> Hairs => Set<Hair>();
    public DbSet<Jelly> Jellies => Set<Jelly>();
    public DbSet<Key> Keys => Set<Key>();
    public DbSet<Leather> Leathers => Set<Leather>();
    public DbSet<Leg> Legs => Set<Leg>();
    public DbSet<Meat> Meats => Set<Meat>();
    public DbSet<Metaria> Metarias => Set<Metaria>();
    public DbSet<Oil> Oils => Set<Oil>();
    public DbSet<Ore> Ores => Set<Ore>();
    public DbSet<PetGhost> PetGhosts => Set<PetGhost>();
    public DbSet<Plank> Planks => Set<Plank>();
    public DbSet<Powder> Powders => Set<Powder>();
    public DbSet<PreciousStone> PreciousStones => Set<PreciousStone>();
    public DbSet<PreservedMeat> PreservedMeats => Set<PreservedMeat>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<Root> Roots => Set<Root>();
    public DbSet<Seed> Seeds => Set<Seed>();
    public DbSet<Shell> Shells => Set<Shell>();
    public DbSet<Skin> Skins => Set<Skin>();
    public DbSet<SmithMagicPotion> SmithMagicPotions => Set<SmithMagicPotion>();
    public DbSet<SmithMagicRune> SmithMagicRunes => Set<SmithMagicRune>();
    public DbSet<Stone> Stones => Set<Stone>();
    public DbSet<StuffedAnimal> StuffedAnimals => Set<StuffedAnimal>();
    public DbSet<Tail> Tails => Set<Tail>();
    public DbSet<Vegetable> Vegetables => Set<Vegetable>();
    public DbSet<Wing> Wings => Set<Wing>();
    public DbSet<Wood> Woods => Set<Wood>();
    public DbSet<Wool> Wools => Set<Wool>();
    
    // Todo refacto -> would make more sense to have a table for each type of card there is
    // Cards
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<CardFamily> CardFamilies => Set<CardFamily>();
    public DbSet<CardFamilyName> CardFamilyNames => Set<CardFamilyName>();
    
    // Drops
    public DbSet<Drop> Drops => Set<Drop>();
    
    // Monsters
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