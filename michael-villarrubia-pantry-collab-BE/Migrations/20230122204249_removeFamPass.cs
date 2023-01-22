using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace michael_villarrubia_pantry_collab_BE.Migrations
{
    public partial class removeFamPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Families");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Families",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
