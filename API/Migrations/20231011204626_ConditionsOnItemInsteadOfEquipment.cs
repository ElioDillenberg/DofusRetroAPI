using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConditionsOnItemInsteadOfEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentEffects_Pets_PetId",
                table: "EquipmentEffects");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_EquipmentEffects_EquipmentEffectId",
                table: "PetFoods");

            migrationBuilder.DropTable(
                name: "EquipmentConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentEffects",
                table: "EquipmentEffects");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Effects",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Effects",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Effects",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Gears");

            migrationBuilder.DropColumn(
                name: "Effects",
                table: "Gears");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Consumables");

            migrationBuilder.DropColumn(
                name: "Effects",
                table: "Consumables");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Effects",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "EquipmentEffects",
                newName: "ItemEffects");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentEffects_PetId",
                table: "ItemEffects",
                newName: "IX_ItemEffects_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentEffects_EquipmentId",
                table: "ItemEffects",
                newName: "IX_ItemEffects_EquipmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemEffects",
                table: "ItemEffects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItemConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ConditionType = table.Column<int>(type: "int", nullable: false),
                    Sign = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemEffect",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    EffectType = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEffect", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemConditions_ItemId",
                table: "ItemConditions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEffect_ItemId",
                table: "ItemEffect",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEffects_Pets_PetId",
                table: "ItemEffects",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_ItemEffects_EquipmentEffectId",
                table: "PetFoods",
                column: "EquipmentEffectId",
                principalTable: "ItemEffects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemEffects_Pets_PetId",
                table: "ItemEffects");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_ItemEffects_EquipmentEffectId",
                table: "PetFoods");

            migrationBuilder.DropTable(
                name: "ItemConditions");

            migrationBuilder.DropTable(
                name: "ItemEffect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemEffects",
                table: "ItemEffects");

            migrationBuilder.RenameTable(
                name: "ItemEffects",
                newName: "EquipmentEffects");

            migrationBuilder.RenameIndex(
                name: "IX_ItemEffects_PetId",
                table: "EquipmentEffects",
                newName: "IX_EquipmentEffects_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemEffects_EquipmentId",
                table: "EquipmentEffects",
                newName: "IX_EquipmentEffects_EquipmentId");

            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effects",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effects",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effects",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Gears",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effects",
                table: "Gears",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Consumables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effects",
                table: "Consumables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effects",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentEffects",
                table: "EquipmentEffects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EquipmentConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    ConditionType = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: true),
                    Min = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentConditions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentConditions_EquipmentId",
                table: "EquipmentConditions",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentEffects_Pets_PetId",
                table: "EquipmentEffects",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_EquipmentEffects_EquipmentEffectId",
                table: "PetFoods",
                column: "EquipmentEffectId",
                principalTable: "EquipmentEffects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
