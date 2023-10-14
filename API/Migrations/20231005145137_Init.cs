using Microsoft.EntityFrameworkCore.Migrations;

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

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: true),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumables", x => x.Id);
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ecosystem = table.Column<int>(type: "int", nullable: false),
                    Breed = table.Column<int>(type: "int", nullable: false),
                    RelatedMonsterId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monsters_Monsters_RelatedMonsterId",
                        column: x => x.RelatedMonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id");
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
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
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
                name: "MonsterCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MonsterCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonsterCharacteristics_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonsterNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseLocalizedNameSequence]"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonsterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonsterNames_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Gears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: true),
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
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: true),
                    SetId = table.Column<int>(type: "int", nullable: true),
                    SoulEater = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Set_SetId",
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
                name: "SetNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseLocalizedNameSequence]"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Pods = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: true),
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
                    ImprovedMax = table.Column<int>(type: "int", nullable: true),
                    PetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentEffects_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SetBonusEffect",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetBonusId = table.Column<int>(type: "int", nullable: false),
                    EffectType = table.Column<int>(type: "int", nullable: false),
                    Min = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetBonusEffect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetBonusEffect_SetBonuses_SetBonusId",
                        column: x => x.SetBonusId,
                        principalTable: "SetBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "PetFoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentEffectId = table.Column<int>(type: "int", nullable: false),
                    EffectIncrease = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: true),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    MonsterId = table.Column<int>(type: "int", nullable: true),
                    SoulEaterFood_PetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetFoods_EquipmentEffects_EquipmentEffectId",
                        column: x => x.EquipmentEffectId,
                        principalTable: "EquipmentEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetFoods_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetFoods_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PetFoods_Pets_SoulEaterFood_PetId",
                        column: x => x.SoulEaterFood_PetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PetFoods_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Id",
                table: "Cards",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumables_Id",
                table: "Consumables",
                column: "Id",
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
                name: "IX_Gears_Id",
                table: "Gears",
                column: "Id",
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
                name: "IX_MonsterCharacteristics_MonsterId",
                table: "MonsterCharacteristics",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterNames_MonsterId",
                table: "MonsterNames",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_Id",
                table: "Monsters",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_RelatedMonsterId",
                table: "Monsters",
                column: "RelatedMonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_EquipmentEffectId",
                table: "PetFoods",
                column: "EquipmentEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_MonsterId",
                table: "PetFoods",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_PetId",
                table: "PetFoods",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_ResourceId",
                table: "PetFoods",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_SoulEaterFood_PetId",
                table: "PetFoods",
                column: "SoulEaterFood_PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_Id",
                table: "Pets",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_SetId",
                table: "Pets",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ItemId",
                table: "Recipes",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Id",
                table: "Resources",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SetBonusEffect_SetBonusId",
                table: "SetBonusEffect",
                column: "SetBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_SetBonuses_SetId",
                table: "SetBonuses",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_SetNames_SetId",
                table: "SetNames",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponCharacteristic_WeaponId",
                table: "WeaponCharacteristic",
                column: "WeaponId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_Id",
                table: "Weapons",
                column: "Id",
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
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Consumables");

            migrationBuilder.DropTable(
                name: "Drops");

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
                name: "MonsterCharacteristics");

            migrationBuilder.DropTable(
                name: "MonsterNames");

            migrationBuilder.DropTable(
                name: "PetFoods");

            migrationBuilder.DropTable(
                name: "SetBonusEffect");

            migrationBuilder.DropTable(
                name: "SetNames");

            migrationBuilder.DropTable(
                name: "WeaponCharacteristic");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "EquipmentEffects");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "SetBonuses");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropSequence(
                name: "BaseLocalizedNameSequence");
        }
    }
}
