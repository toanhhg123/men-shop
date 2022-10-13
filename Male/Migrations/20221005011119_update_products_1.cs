using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Male.Migrations
{
    public partial class update_products_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "img2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "img3",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "img4",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "img2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "img3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "img4",
                table: "Products");
        }
    }
}
