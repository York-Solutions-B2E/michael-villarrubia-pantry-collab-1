using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace michael_villarrubia_pantry_collab_BE.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FamilyId",
                table: "Recipes",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Families_FamilyId",
                table: "Recipes",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Families_FamilyId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_FamilyId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "Recipes");
        }
    }
}
