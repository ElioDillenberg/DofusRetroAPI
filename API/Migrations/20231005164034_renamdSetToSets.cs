using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class renamdSetToSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gears_Set_SetId",
                table: "Gears");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Set_SetId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_SetBonuses_Set_SetId",
                table: "SetBonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SetNames_Set_SetId",
                table: "SetNames");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Set_SetId",
                table: "Weapons");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Gears_Sets_SetId",
                table: "Gears",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Sets_SetId",
                table: "Pets",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SetBonuses_Sets_SetId",
                table: "SetBonuses",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SetNames_Sets_SetId",
                table: "SetNames",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Sets_SetId",
                table: "Weapons",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gears_Sets_SetId",
                table: "Gears");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Sets_SetId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_SetBonuses_Sets_SetId",
                table: "SetBonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SetNames_Sets_SetId",
                table: "SetNames");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Sets_SetId",
                table: "Weapons");

            migrationBuilder.DropTable(
                name: "Sets");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Gears_Set_SetId",
                table: "Gears",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Set_SetId",
                table: "Pets",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SetBonuses_Set_SetId",
                table: "SetBonuses",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SetNames_Set_SetId",
                table: "SetNames",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Set_SetId",
                table: "Weapons",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id");
        }
    }
}
