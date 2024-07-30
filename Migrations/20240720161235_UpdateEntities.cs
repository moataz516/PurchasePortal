using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurchasePortal.Web.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritItems_Favorites_FavoriteUserId",
                table: "FavoritItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritItems_Users_ApplicationUserId",
                table: "FavoritItems");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_FavoritItems_ApplicationUserId",
                table: "FavoritItems");

            migrationBuilder.DropIndex(
                name: "IX_FavoritItems_FavoriteUserId_ProductId",
                table: "FavoritItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipping",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FavoritItems");

            migrationBuilder.RenameTable(
                name: "Shipping",
                newName: "Shippings");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "FavoriteUserId",
                table: "FavoritItems",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Shippings",
                newName: "State");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Products",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "OrderId1",
                table: "Payments",
                type: "NVARCHAR2(450)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "OrderItems",
                type: "NVARCHAR2(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Shippings",
                type: "NVARCHAR2(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Shippings",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Shippings",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Shippings",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Shippings",
                type: "NVARCHAR2(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Shippings",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippedDate",
                table: "Shippings",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Shippings",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Shippings",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shippings",
                table: "Shippings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    OrderId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    BillingAddress = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    City = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    State = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PostalCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Country = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billings_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ProductId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId1",
                table: "Payments",
                column: "OrderId1",
                unique: true,
                filter: "\"OrderId1\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritItems_UserId_ProductId",
                table: "FavoritItems",
                columns: new[] { "UserId", "ProductId" },
                unique: true,
                filter: "\"UserId\" IS NOT NULL AND \"ProductId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_OrderId",
                table: "Shippings",
                column: "OrderId",
                unique: true,
                filter: "\"OrderId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_OrderId",
                table: "Billings",
                column: "OrderId",
                unique: true,
                filter: "\"OrderId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId_ProductId",
                table: "CartItems",
                columns: new[] { "UserId", "ProductId" },
                unique: true,
                filter: "\"UserId\" IS NOT NULL AND \"ProductId\" IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritItems_Users_UserId",
                table: "FavoritItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId1",
                table: "Payments",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Orders_OrderId",
                table: "Shippings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritItems_Users_UserId",
                table: "FavoritItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId1",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Orders_OrderId",
                table: "Shippings");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_FavoritItems_UserId_ProductId",
                table: "FavoritItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shippings",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_OrderId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "ShippedDate",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Shippings");

            migrationBuilder.RenameTable(
                name: "Shippings",
                newName: "Shipping");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoritItems",
                newName: "FavoriteUserId");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Shipping",
                newName: "Name");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Products",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Payments",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "OrderItems",
                type: "NVARCHAR2(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FavoritItems",
                type: "NVARCHAR2(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Shipping",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipping",
                table: "Shipping",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TotalFavorites = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoritItems_ApplicationUserId",
                table: "FavoritItems",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritItems_FavoriteUserId_ProductId",
                table: "FavoritItems",
                columns: new[] { "FavoriteUserId", "ProductId" },
                unique: true,
                filter: "\"FavoriteUserId\" IS NOT NULL AND \"ProductId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritItems_Favorites_FavoriteUserId",
                table: "FavoritItems",
                column: "FavoriteUserId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritItems_Users_ApplicationUserId",
                table: "FavoritItems",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
