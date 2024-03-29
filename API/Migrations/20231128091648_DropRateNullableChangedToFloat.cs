using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class DropRateNullableChangedToFloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropTableId",
                table: "Drops");

            migrationBuilder.AlterColumn<float>(
                name: "Rate",
                table: "Drops",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "Drops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DropTableId",
                table: "Drops",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
