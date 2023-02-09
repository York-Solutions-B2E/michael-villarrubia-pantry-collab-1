using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace michael_villarrubia_pantry_collab_BE.Migrations
{
    public partial class itemUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "PantryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "PantryItems");
        }
    }
}
