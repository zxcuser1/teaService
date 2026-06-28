using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseToAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewModerationsStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsModerated",
                table: "Teas");

            migrationBuilder.DropColumn(
                name: "IsModerated",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "ModerationStatus",
                table: "Teas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModerationStatus",
                table: "Ingredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerationStatus",
                table: "Teas");

            migrationBuilder.DropColumn(
                name: "ModerationStatus",
                table: "Ingredients");

            migrationBuilder.AddColumn<bool>(
                name: "IsModerated",
                table: "Teas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsModerated",
                table: "Ingredients",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
