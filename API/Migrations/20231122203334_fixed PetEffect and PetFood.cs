using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixedPetEffectandPetFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentEffect_Pets_PetId",
                table: "EquipmentEffect");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_EquipmentEffect_EquipmentEffectId",
                table: "PetFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_Monsters_MonsterId",
                table: "PetFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_Pets_PetId",
                table: "PetFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_Pets_SoulEaterFood_PetId",
                table: "PetFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_Resources_ResourceId",
                table: "PetFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetFoods",
                table: "PetFoods");

            migrationBuilder.DropIndex(
                name: "IX_PetFoods_ResourceId",
                table: "PetFoods");

            migrationBuilder.DropIndex(
                name: "IX_PetFoods_SoulEaterFood_PetId",
                table: "PetFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentEffect",
                table: "EquipmentEffect");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentEffect_EquipmentId",
                table: "EquipmentEffect");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "PetFoods");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "PetFoods");

            migrationBuilder.DropColumn(
                name: "SoulEaterFood_PetId",
                table: "PetFoods");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "EquipmentEffect");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "EquipmentEffect");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "EquipmentEffect");

            migrationBuilder.DropColumn(
                name: "Min",
                table: "EquipmentEffect");

            migrationBuilder.RenameTable(
                name: "PetFoods",
                newName: "SoulEaterFood");

            migrationBuilder.RenameTable(
                name: "EquipmentEffect",
                newName: "PetEffect");

            migrationBuilder.RenameColumn(
                name: "EquipmentEffectId",
                table: "SoulEaterFood",
                newName: "PetEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_PetFoods_PetId",
                table: "SoulEaterFood",
                newName: "IX_SoulEaterFood_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_PetFoods_MonsterId",
                table: "SoulEaterFood",
                newName: "IX_SoulEaterFood_MonsterId");

            migrationBuilder.RenameIndex(
                name: "IX_PetFoods_EquipmentEffectId",
                table: "SoulEaterFood",
                newName: "IX_SoulEaterFood_PetEffectId");

            migrationBuilder.RenameColumn(
                name: "StatType",
                table: "PetEffect",
                newName: "ItemEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentEffect_PetId",
                table: "PetEffect",
                newName: "IX_PetEffect_PetId");

            migrationBuilder.AlterColumn<int>(
                name: "MonsterId",
                table: "SoulEaterFood",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImprovedMax",
                table: "PetEffect",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoulEaterFood",
                table: "SoulEaterFood",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetEffect",
                table: "PetEffect",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ResourceEaterFood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    PetEffectId = table.Column<int>(type: "int", nullable: false),
                    EffectIncrease = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceEaterFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceEaterFood_PetEffect_PetEffectId",
                        column: x => x.PetEffectId,
                        principalTable: "PetEffect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceEaterFood_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceEaterFood_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetEffect_ItemEffectId",
                table: "PetEffect",
                column: "ItemEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceEaterFood_PetEffectId",
                table: "ResourceEaterFood",
                column: "PetEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceEaterFood_PetId",
                table: "ResourceEaterFood",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceEaterFood_ResourceId",
                table: "ResourceEaterFood",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetEffect_ItemEffects_ItemEffectId",
                table: "PetEffect",
                column: "ItemEffectId",
                principalTable: "ItemEffects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetEffect_Pets_PetId",
                table: "PetEffect",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoulEaterFood_Monsters_MonsterId",
                table: "SoulEaterFood",
                column: "MonsterId",
                principalTable: "Monsters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoulEaterFood_PetEffect_PetEffectId",
                table: "SoulEaterFood",
                column: "PetEffectId",
                principalTable: "PetEffect",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoulEaterFood_Pets_PetId",
                table: "SoulEaterFood",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetEffect_ItemEffects_ItemEffectId",
                table: "PetEffect");

            migrationBuilder.DropForeignKey(
                name: "FK_PetEffect_Pets_PetId",
                table: "PetEffect");

            migrationBuilder.DropForeignKey(
                name: "FK_SoulEaterFood_Monsters_MonsterId",
                table: "SoulEaterFood");

            migrationBuilder.DropForeignKey(
                name: "FK_SoulEaterFood_PetEffect_PetEffectId",
                table: "SoulEaterFood");

            migrationBuilder.DropForeignKey(
                name: "FK_SoulEaterFood_Pets_PetId",
                table: "SoulEaterFood");

            migrationBuilder.DropTable(
                name: "ResourceEaterFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoulEaterFood",
                table: "SoulEaterFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetEffect",
                table: "PetEffect");

            migrationBuilder.DropIndex(
                name: "IX_PetEffect_ItemEffectId",
                table: "PetEffect");

            migrationBuilder.RenameTable(
                name: "SoulEaterFood",
                newName: "PetFoods");

            migrationBuilder.RenameTable(
                name: "PetEffect",
                newName: "EquipmentEffect");

            migrationBuilder.RenameColumn(
                name: "PetEffectId",
                table: "PetFoods",
                newName: "EquipmentEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_SoulEaterFood_PetId",
                table: "PetFoods",
                newName: "IX_PetFoods_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_SoulEaterFood_PetEffectId",
                table: "PetFoods",
                newName: "IX_PetFoods_EquipmentEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_SoulEaterFood_MonsterId",
                table: "PetFoods",
                newName: "IX_PetFoods_MonsterId");

            migrationBuilder.RenameColumn(
                name: "ItemEffectId",
                table: "EquipmentEffect",
                newName: "StatType");

            migrationBuilder.RenameIndex(
                name: "IX_PetEffect_PetId",
                table: "EquipmentEffect",
                newName: "IX_EquipmentEffect_PetId");

            migrationBuilder.AlterColumn<int>(
                name: "MonsterId",
                table: "PetFoods",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "PetFoods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "PetFoods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoulEaterFood_PetId",
                table: "PetFoods",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImprovedMax",
                table: "EquipmentEffect",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "EquipmentEffect",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "EquipmentEffect",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Max",
                table: "EquipmentEffect",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Min",
                table: "EquipmentEffect",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetFoods",
                table: "PetFoods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentEffect",
                table: "EquipmentEffect",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_ResourceId",
                table: "PetFoods",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_SoulEaterFood_PetId",
                table: "PetFoods",
                column: "SoulEaterFood_PetId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentEffect_EquipmentId",
                table: "EquipmentEffect",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentEffect_Pets_PetId",
                table: "EquipmentEffect",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_EquipmentEffect_EquipmentEffectId",
                table: "PetFoods",
                column: "EquipmentEffectId",
                principalTable: "EquipmentEffect",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_Monsters_MonsterId",
                table: "PetFoods",
                column: "MonsterId",
                principalTable: "Monsters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_Pets_PetId",
                table: "PetFoods",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_Pets_SoulEaterFood_PetId",
                table: "PetFoods",
                column: "SoulEaterFood_PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_Resources_ResourceId",
                table: "PetFoods",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
