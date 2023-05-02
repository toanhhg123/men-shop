using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Male.Migrations
{
    public partial class update_banner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "subTitle",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subTitle",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "title",
                table: "Banners");
        }
    }
}
