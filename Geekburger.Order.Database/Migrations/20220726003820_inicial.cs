using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geekburger.Order.Database.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoreName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Total = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrdersPayments",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequesterId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StoreName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PayType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CardNumber = table.Column<string>(type: "TEXT", maxLength: 16, nullable: true),
                    CardOwnerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SecurityCode = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersPayments", x => new { x.OrderId, x.RequesterId });
                    table.ForeignKey(
                        name: "FK_OrdersPayments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersProduction",
                columns: table => new
                {
                    ProductionId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProduction", x => new { x.OrderId, x.ProductionId });
                    table.ForeignKey(
                        name: "FK_OrdersProduction_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersProducts",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersPayments");

            migrationBuilder.DropTable(
                name: "OrdersProduction");

            migrationBuilder.DropTable(
                name: "OrdersProducts");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
