﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "BaseLocalizedNameSequence");

            migrationBuilder.CreateSequence(
                name: "BaseMonsterSequence");

            migrationBuilder.CreateSequence(
                name: "ItemSequence");

            migrationBuilder.CreateTable(
                name: "BaseResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ItemSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: false),
                    ResourceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ItemSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    CardRarity = table.Column<int>(type: "int", nullable: false),
                    CardFamily = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consumables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ItemSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: false),
                    ConsumableType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ecosystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecosystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    ConditionType = table.Column<int>(type: "int", nullable: false),
                    Min = table.Column<int>(type: "int", nullable: true),
                    Max = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseLocalizedNameSequence]"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonsterCharacteristic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonsterId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    HealthPoints = table.Column<int>(type: "int", nullable: false),
                    ActionPoints = table.Column<int>(type: "int", nullable: false),
                    MovementPoints = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Initiative = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Chance = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    ActionPointAvoidance = table.Column<int>(type: "int", nullable: false),
                    MovementPointAvoidance = table.Column<int>(type: "int", nullable: false),
                    NeutralResistancePercentage = table.Column<int>(type: "int", nullable: false),
                    EarthResistancePercentage = table.Column<int>(type: "int", nullable: false),
                    FireResistancePercentage = table.Column<int>(type: "int", nullable: false),
                    WaterResistancePercentage = table.Column<int>(type: "int", nullable: false),
                    AirResistancePercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterCharacteristic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    IsSecret = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BreedNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseLocalizedNameSequence]"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BreedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreedNames_Breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcosystemNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EcosystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcosystemNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcosystemNames_Ecosystems_EcosystemId",
                        column: x => x.EcosystemId,
                        principalTable: "Ecosystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseMonsterSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    EcosystemId = table.Column<int>(type: "int", nullable: true),
                    BreedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monsters_Breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breeds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Monsters_Ecosystems_EcosystemId",
                        column: x => x.EcosystemId,
                        principalTable: "Ecosystems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodEaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ItemSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: false),
                    EquipmentType = table.Column<int>(type: "int", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodEaters_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ItemSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: false),
                    EquipmentType = table.Column<int>(type: "int", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gears_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SetBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    NumberOfItems = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetBonuses_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetEffects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    EffectType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetEffects_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseLocalizedNameSequence]"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetNames_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoulEaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ItemSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: false),
                    EquipmentType = table.Column<int>(type: "int", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoulEaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoulEaters_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ItemSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: false),
                    EquipmentType = table.Column<int>(type: "int", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubAreaNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseLocalizedNameSequence]"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubAreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAreaNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAreaNames_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchMonsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseMonsterSequence]"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    EcosystemId = table.Column<int>(type: "int", nullable: true),
                    BreedId = table.Column<int>(type: "int", nullable: true),
                    MonsterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchMonsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchMonsters_Breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breeds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchMonsters_Ecosystems_EcosystemId",
                        column: x => x.EcosystemId,
                        principalTable: "Ecosystems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchMonsters_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DropTableId = table.Column<int>(type: "int", nullable: false),
                    MonsterId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    DropCap = table.Column<int>(type: "int", nullable: true),
                    ProspectionThreshold = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drops_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonsterSubArea",
                columns: table => new
                {
                    MonstersId = table.Column<int>(type: "int", nullable: false),
                    SubAreasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterSubArea", x => new { x.MonstersId, x.SubAreasId });
                    table.ForeignKey(
                        name: "FK_MonsterSubArea_Monsters_MonstersId",
                        column: x => x.MonstersId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonsterSubArea_SubAreas_SubAreasId",
                        column: x => x.SubAreasId,
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentEffects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    EffectType = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: false),
                    Min = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetBonusId = table.Column<int>(type: "int", nullable: true),
                    ImprovedMax = table.Column<int>(type: "int", nullable: true),
                    PetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentEffects_SetBonuses_SetBonusId",
                        column: x => x.SetBonusId,
                        principalTable: "SetBonuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeaponCharacteristic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeaponId = table.Column<int>(type: "int", nullable: false),
                    ActionPoints = table.Column<int>(type: "int", nullable: false),
                    CriticalStrikeBonus = table.Column<int>(type: "int", nullable: false),
                    CriticalStrikeBaseChance = table.Column<int>(type: "int", nullable: false),
                    MinRange = table.Column<int>(type: "int", nullable: false),
                    MaxRange = table.Column<int>(type: "int", nullable: false),
                    OneHand = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponCharacteristic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeaponCharacteristic_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonsterNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseLocalizedNameSequence]"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MonsterId = table.Column<int>(type: "int", nullable: false),
                    ArchMonsterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonsterNames_ArchMonsters_ArchMonsterId",
                        column: x => x.ArchMonsterId,
                        principalTable: "ArchMonsters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MonsterNames_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetFoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentEffectId = table.Column<int>(type: "int", nullable: false),
                    EffectIncrease = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: true),
                    ResourceEaterId = table.Column<int>(type: "int", nullable: true),
                    MonsterId = table.Column<int>(type: "int", nullable: true),
                    SoulEaterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetFoods_BaseResources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "BaseResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetFoods_EquipmentEffects_EquipmentEffectId",
                        column: x => x.EquipmentEffectId,
                        principalTable: "EquipmentEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetFoods_FoodEaters_ResourceEaterId",
                        column: x => x.ResourceEaterId,
                        principalTable: "FoodEaters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PetFoods_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetFoods_SoulEaters_SoulEaterId",
                        column: x => x.SoulEaterId,
                        principalTable: "SoulEaters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchMonsters_BreedId",
                table: "ArchMonsters",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchMonsters_EcosystemId",
                table: "ArchMonsters",
                column: "EcosystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchMonsters_MonsterId",
                table: "ArchMonsters",
                column: "MonsterId",
                unique: true,
                filter: "[MonsterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BaseResources_Image",
                table: "BaseResources",
                column: "Image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BreedNames_BreedId",
                table: "BreedNames",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedNames_Name",
                table: "BreedNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Image",
                table: "Cards",
                column: "Image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumables_Image",
                table: "Consumables",
                column: "Image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drops_ItemId",
                table: "Drops",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Drops_MonsterId",
                table: "Drops",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_EcosystemNames_EcosystemId",
                table: "EcosystemNames",
                column: "EcosystemId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentConditions_EquipmentId",
                table: "EquipmentConditions",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentEffects_EquipmentId",
                table: "EquipmentEffects",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentEffects_PetId",
                table: "EquipmentEffects",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentEffects_SetBonusId",
                table: "EquipmentEffects",
                column: "SetBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEaters_Image",
                table: "FoodEaters",
                column: "Image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodEaters_SetId",
                table: "FoodEaters",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Gears_Image",
                table: "Gears",
                column: "Image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gears_SetId",
                table: "Gears",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ItemId",
                table: "Ingredients",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDescriptions_ItemId",
                table: "ItemDescriptions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemNames_ItemId",
                table: "ItemNames",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemNames_Name",
                table: "ItemNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonsterCharacteristic_MonsterId",
                table: "MonsterCharacteristic",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterNames_ArchMonsterId",
                table: "MonsterNames",
                column: "ArchMonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterNames_MonsterId",
                table: "MonsterNames",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterNames_Name",
                table: "MonsterNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_BreedId",
                table: "Monsters",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_EcosystemId",
                table: "Monsters",
                column: "EcosystemId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterSubArea_SubAreasId",
                table: "MonsterSubArea",
                column: "SubAreasId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_EquipmentEffectId",
                table: "PetFoods",
                column: "EquipmentEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_MonsterId",
                table: "PetFoods",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_ResourceEaterId",
                table: "PetFoods",
                column: "ResourceEaterId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_ResourceId",
                table: "PetFoods",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_SoulEaterId",
                table: "PetFoods",
                column: "SoulEaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ItemId",
                table: "Recipes",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SetBonuses_SetId",
                table: "SetBonuses",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_SetEffects_SetId",
                table: "SetEffects",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_SetNames_Name",
                table: "SetNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SetNames_SetId",
                table: "SetNames",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_SoulEaters_Image",
                table: "SoulEaters",
                column: "Image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoulEaters_SetId",
                table: "SoulEaters",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAreaNames_Name",
                table: "SubAreaNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubAreaNames_SubAreaId",
                table: "SubAreaNames",
                column: "SubAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponCharacteristic_WeaponId",
                table: "WeaponCharacteristic",
                column: "WeaponId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_Image",
                table: "Weapons",
                column: "Image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_SetId",
                table: "Weapons",
                column: "SetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreedNames");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Consumables");

            migrationBuilder.DropTable(
                name: "Drops");

            migrationBuilder.DropTable(
                name: "EcosystemNames");

            migrationBuilder.DropTable(
                name: "EquipmentConditions");

            migrationBuilder.DropTable(
                name: "Gears");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "ItemDescriptions");

            migrationBuilder.DropTable(
                name: "ItemNames");

            migrationBuilder.DropTable(
                name: "MonsterCharacteristic");

            migrationBuilder.DropTable(
                name: "MonsterNames");

            migrationBuilder.DropTable(
                name: "MonsterSubArea");

            migrationBuilder.DropTable(
                name: "PetFoods");

            migrationBuilder.DropTable(
                name: "SetEffects");

            migrationBuilder.DropTable(
                name: "SetNames");

            migrationBuilder.DropTable(
                name: "SubAreaNames");

            migrationBuilder.DropTable(
                name: "WeaponCharacteristic");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "ArchMonsters");

            migrationBuilder.DropTable(
                name: "BaseResources");

            migrationBuilder.DropTable(
                name: "EquipmentEffects");

            migrationBuilder.DropTable(
                name: "FoodEaters");

            migrationBuilder.DropTable(
                name: "SoulEaters");

            migrationBuilder.DropTable(
                name: "SubAreas");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "SetBonuses");

            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "Ecosystems");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropSequence(
                name: "BaseLocalizedNameSequence");

            migrationBuilder.DropSequence(
                name: "BaseMonsterSequence");

            migrationBuilder.DropSequence(
                name: "ItemSequence");
        }
    }
}