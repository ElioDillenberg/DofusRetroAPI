using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class NamesNotUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SetNames_Name",
                table: "SetNames");

            migrationBuilder.DropIndex(
                name: "IX_MonsterNames_Name",
                table: "MonsterNames");

            migrationBuilder.DropIndex(
                name: "IX_ItemNames_Name",
                table: "ItemNames");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SetNames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MonsterNames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemNames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SetNames",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MonsterNames",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemNames",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SetNames_Name",
                table: "SetNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonsterNames_Name",
                table: "MonsterNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemNames_Name",
                table: "ItemNames",
                column: "Name",
                unique: true);
        }
    }
}
