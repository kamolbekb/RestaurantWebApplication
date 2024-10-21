using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebRestaurantManagement.Entities
{
    /// <inheritdoc />
    public partial class InItWebRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MealsCount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Waiters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_waiters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Describtion = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meals", x => x.Id);
                    table.ForeignKey(
                        name: "fk_meals_categories",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDateTime = table.Column<DateOnly>(type: "date", nullable: true),
                    WaiterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.Id);
                    table.ForeignKey(
                        name: "fk_orders_waiters",
                        column: x => x.WaiterId,
                        principalTable: "Waiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    MealId = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_details", x => new { x.OrderId, x.MealId });
                    table.ForeignKey(
                        name: "fk_order_details_meals",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_order_details_orders",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "MealsCount", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Appetizers" },
                    { 2, 3, "Main Courses" }
                });

            migrationBuilder.InsertData(
                table: "Waiters",
                columns: new[] { "Id", "Address", "HireDate", "Name" },
                values: new object[,]
                {
                    { 1, "123 Main St", new DateOnly(2020, 1, 10), "John Doe" },
                    { 2, "456 Elm St", new DateOnly(2019, 5, 20), "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "CategoryId", "Describtion", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Grilled bread with toppings", "Bruschetta" },
                    { 2, 1, "Toasted bread with garlic", "Garlic Bread" },
                    { 3, 2, "Chicken with herbs", "Grilled Chicken" },
                    { 4, 2, "Creamy pasta with bacon", "Pasta Carbonara" },
                    { 5, 2, "Grilled beef steak", "Steak" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDateTime", "WaiterId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 10, 1), 1 },
                    { 2, new DateOnly(2023, 10, 2), 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "MealId", "OrderId", "Count", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 5.5 },
                    { 3, 1, 1, 12.0 },
                    { 4, 2, 1, 8.5 },
                    { 5, 2, 2, 15.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_CategoryId",
                table: "Meals",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MealId",
                table: "OrderDetails",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WaiterId",
                table: "Orders",
                column: "WaiterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Waiters");
        }
    }
}
