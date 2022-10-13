using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Male.Migrations
{
    public partial class Model_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "newid()"),
                    accountid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    isConfirm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_accountid",
                        column: x => x.accountid,
                        principalTable: "Accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_accountid",
                table: "Orders",
                column: "accountid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_productid",
                table: "Orders",
                column: "productid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
