using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dishcover.Migrations
{
    public partial class RecipeCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeCollectionId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeCollections_AspNetUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeCollectionId",
                table: "Recipes",
                column: "RecipeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCollections_Userid",
                table: "RecipeCollections",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeCollections_RecipeCollectionId",
                table: "Recipes",
                column: "RecipeCollectionId",
                principalTable: "RecipeCollections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeCollections_RecipeCollectionId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeCollections");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RecipeCollectionId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeCollectionId",
                table: "Recipes");
        }
    }
}
