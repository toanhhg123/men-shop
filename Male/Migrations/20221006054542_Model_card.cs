using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Male.Migrations
{
    public partial class Model_card : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    Accountid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Carts_Accounts_Accountid",
                        column: x => x.Accountid,
                        principalTable: "Accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Accountid",
                table: "Carts",
                column: "Accountid");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_productid",
                table: "Carts",
                column: "productid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");
        }
    }
}
