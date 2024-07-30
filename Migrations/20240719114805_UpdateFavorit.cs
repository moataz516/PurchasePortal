using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurchasePortal.Web.Migrations
{
    public partial class UpdateFavorit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritItems_Users_UserId",
                table: "FavoritItems");

            migrationBuilder.DropIndex(
                name: "IX_FavoritItems_UserId_ProductId",
                table: "FavoritItems");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoritItems",
                newName: "FavoriteUserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Products",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FavoritItems",
                type: "NVARCHAR2(450)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritItems_Favorites_FavoriteUserId",
                table: "FavoritItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritItems_Users_ApplicationUserId",
                table: "FavoritItems");

            migrationBuilder.DropIndex(
                name: "IX_FavoritItems_ApplicationUserId",
                table: "FavoritItems");

            migrationBuilder.DropIndex(
                name: "IX_FavoritItems_FavoriteUserId_ProductId",
                table: "FavoritItems");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FavoritItems");

            migrationBuilder.RenameColumn(
                name: "FavoriteUserId",
                table: "FavoritItems",
                newName: "UserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Products",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritItems_UserId_ProductId",
                table: "FavoritItems",
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
        }
    }
}
