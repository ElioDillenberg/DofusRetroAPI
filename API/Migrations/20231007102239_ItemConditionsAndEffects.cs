using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class ItemConditionsAndEffects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
