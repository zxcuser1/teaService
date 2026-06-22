using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseToAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIngredientImageAndSuggestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Teas",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Ingredients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SuggestedUserId",
                table: "Ingredients",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "SuggestedUserId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Teas",
                newName: "Image");
        }
    }
}
