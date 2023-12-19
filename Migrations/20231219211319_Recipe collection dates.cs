using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dishcover.Migrations
{
    public partial class Recipecollectiondates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RecipeCollections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RecipeCollections",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RecipeCollections");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RecipeCollections");
        }
    }
}
