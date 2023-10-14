using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriceToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Weapons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Gears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Consumables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Gears");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Consumables");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cards");
        }
    }
}
