using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSecretPropertyFromRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PK_PetEffect",
                table: "PetEffect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoulEaterFood",
                table: "SoulEaterFood");

            migrationBuilder.DropColumn(
                name: "IsSecret",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "PetEffect",
                newName: "PetEffects");

            migrationBuilder.RenameTable(
                name: "SoulEaterFood",
                newName: "PetFoods");

            migrationBuilder.RenameIndex(
                name: "IX_PetEffect_PetId",
                table: "PetEffects",
                newName: "IX_PetEffects_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_PetEffect_ItemEffectId",
                table: "PetEffects",
                newName: "IX_PetEffects_ItemEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_SoulEaterFood_PetId",
                table: "PetFoods",
                newName: "IX_PetFoods_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_SoulEaterFood_PetEffectId",
                table: "PetFoods",
                newName: "IX_PetFoods_PetEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_SoulEaterFood_MonsterId",
                table: "PetFoods",
                newName: "IX_PetFoods_MonsterId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetEffects",
                table: "PetEffects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetFoods",
                table: "PetFoods",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_ResourceId",
                table: "PetFoods",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFoods_SoulEaterFood_PetId",
                table: "PetFoods",
                column: "SoulEaterFood_PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetEffects_ItemEffects_ItemEffectId",
                table: "PetEffects",
                column: "ItemEffectId",
                principalTable: "ItemEffects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetEffects_Pets_PetId",
                table: "PetEffects",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_Monsters_MonsterId",
                table: "PetFoods",
                column: "MonsterId",
                principalTable: "Monsters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetFoods_PetEffects_PetEffectId",
                table: "PetFoods",
                column: "PetEffectId",
                principalTable: "PetEffects",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetEffects_ItemEffects_ItemEffectId",
                table: "PetEffects");

            migrationBuilder.DropForeignKey(
                name: "FK_PetEffects_Pets_PetId",
                table: "PetEffects");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_Monsters_MonsterId",
                table: "PetFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_PetFoods_PetEffects_PetEffectId",
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
                name: "PK_PetEffects",
                table: "PetEffects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetFoods",
                table: "PetFoods");

            migrationBuilder.DropIndex(
                name: "IX_PetFoods_ResourceId",
                table: "PetFoods");

            migrationBuilder.DropIndex(
                name: "IX_PetFoods_SoulEaterFood_PetId",
                table: "PetFoods");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "PetFoods");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "PetFoods");

            migrationBuilder.DropColumn(
                name: "SoulEaterFood_PetId",
                table: "PetFoods");

            migrationBuilder.RenameTable(
                name: "PetEffects",
                newName: "PetEffect");

            migrationBuilder.RenameTable(
                name: "PetFoods",
                newName: "SoulEaterFood");

            migrationBuilder.RenameIndex(
                name: "IX_PetEffects_PetId",
                table: "PetEffect",
                newName: "IX_PetEffect_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_PetEffects_ItemEffectId",
                table: "PetEffect",
                newName: "IX_PetEffect_ItemEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_PetFoods_PetId",
                table: "SoulEaterFood",
                newName: "IX_SoulEaterFood_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_PetFoods_PetEffectId",
                table: "SoulEaterFood",
                newName: "IX_SoulEaterFood_PetEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_PetFoods_MonsterId",
                table: "SoulEaterFood",
                newName: "IX_SoulEaterFood_MonsterId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSecret",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "MonsterId",
                table: "SoulEaterFood",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetEffect",
                table: "PetEffect",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoulEaterFood",
                table: "SoulEaterFood",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ResourceEaterFood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetEffectId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    EffectIncrease = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: true)
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
    }
}
