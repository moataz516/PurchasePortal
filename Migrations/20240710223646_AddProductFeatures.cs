using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurchasePortal.Web.Migrations
{
    public partial class AddProductFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Products",
                type: "DECIMAL(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantitySold",
                table: "Products",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.CreateTable(
                name: "PromotionCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPromotions",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    PromotionCategoryId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Id = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPromotions", x => new { x.ProductId, x.PromotionCategoryId });
                    table.ForeignKey(
                        name: "FK_ProductPromotions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPromotions_PromotionCategories_PromotionCategoryId",
                        column: x => x.PromotionCategoryId,
                        principalTable: "PromotionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPromotions_PromotionCategoryId",
                table: "ProductPromotions",
                column: "PromotionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPromotions");

            migrationBuilder.DropTable(
                name: "PromotionCategories");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QuantitySold",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
        }
    }
}
