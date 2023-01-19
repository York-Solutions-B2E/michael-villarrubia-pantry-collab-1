using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace michael_villarrubia_pantry_collab_BE.Migrations
{
    public partial class pantry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pantry_Families_FamilyId",
                table: "Pantry");

            migrationBuilder.DropForeignKey(
                name: "FK_PantryItem_Pantry_PantryId",
                table: "PantryItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PantryItem",
                table: "PantryItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pantry",
                table: "Pantry");

            migrationBuilder.RenameTable(
                name: "PantryItem",
                newName: "PantryItems");

            migrationBuilder.RenameTable(
                name: "Pantry",
                newName: "Pantries");

            migrationBuilder.RenameIndex(
                name: "IX_PantryItem_PantryId",
                table: "PantryItems",
                newName: "IX_PantryItems_PantryId");

            migrationBuilder.RenameIndex(
                name: "IX_Pantry_FamilyId",
                table: "Pantries",
                newName: "IX_Pantries_FamilyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PantryItems",
                table: "PantryItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pantries",
                table: "Pantries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pantries_Families_FamilyId",
                table: "Pantries",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PantryItems_Pantries_PantryId",
                table: "PantryItems",
                column: "PantryId",
                principalTable: "Pantries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pantries_Families_FamilyId",
                table: "Pantries");

            migrationBuilder.DropForeignKey(
                name: "FK_PantryItems_Pantries_PantryId",
                table: "PantryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PantryItems",
                table: "PantryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pantries",
                table: "Pantries");

            migrationBuilder.RenameTable(
                name: "PantryItems",
                newName: "PantryItem");

            migrationBuilder.RenameTable(
                name: "Pantries",
                newName: "Pantry");

            migrationBuilder.RenameIndex(
                name: "IX_PantryItems_PantryId",
                table: "PantryItem",
                newName: "IX_PantryItem_PantryId");

            migrationBuilder.RenameIndex(
                name: "IX_Pantries_FamilyId",
                table: "Pantry",
                newName: "IX_Pantry_FamilyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PantryItem",
                table: "PantryItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pantry",
                table: "Pantry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pantry_Families_FamilyId",
                table: "Pantry",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PantryItem_Pantry_PantryId",
                table: "PantryItem",
                column: "PantryId",
                principalTable: "Pantry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
