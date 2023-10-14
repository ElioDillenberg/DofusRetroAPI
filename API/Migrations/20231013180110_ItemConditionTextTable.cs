using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class ItemConditionTextTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SetNames",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MonsterNames",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ItemNames",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Sign",
                table: "ItemConditions",
                newName: "ConditionSign");

            migrationBuilder.CreateTable(
                name: "ItemConditionTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseLocalizedNameSequence]"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemConditionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemConditionTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemConditionTexts_ItemConditions_ItemConditionId",
                        column: x => x.ItemConditionId,
                        principalTable: "ItemConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemConditionTexts_ItemConditionId",
                table: "ItemConditionTexts",
                column: "ItemConditionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemConditionTexts");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "SetNames",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "MonsterNames",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "ItemNames",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ConditionSign",
                table: "ItemConditions",
                newName: "Sign");
        }
    }
}
