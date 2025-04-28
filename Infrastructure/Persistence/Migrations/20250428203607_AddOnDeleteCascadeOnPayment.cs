using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOnDeleteCascadeOnPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliveryMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deliveryMethodId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PaymentIntendId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_deliveryMethods_deliveryMethodId",
                        column: x => x.deliveryMethodId,
                        principalTable: "deliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_ProductId = table.Column<int>(type: "int", nullable: false),
                    product_ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderItems_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_OrderId",
                table: "orderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_deliveryMethodId",
                table: "orders",
                column: "deliveryMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "deliveryMethods");
        }
    }
}
