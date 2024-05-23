using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MushroomPocket.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureInventoryItemRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Items_ItemsId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_ItemsId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "ItemsId",
                table: "Inventory");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ItemId",
                table: "Inventory",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Items_ItemId",
                table: "Inventory",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Items_ItemId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_ItemId",
                table: "Inventory");

            migrationBuilder.AddColumn<int>(
                name: "ItemsId",
                table: "Inventory",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ItemsId",
                table: "Inventory",
                column: "ItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Items_ItemsId",
                table: "Inventory",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
