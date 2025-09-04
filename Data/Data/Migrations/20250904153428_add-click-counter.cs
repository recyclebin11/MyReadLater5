using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addclickcounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClickCounter",
                table: "Bookmark",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickCounter",
                table: "Bookmark");
        }
    }
}
