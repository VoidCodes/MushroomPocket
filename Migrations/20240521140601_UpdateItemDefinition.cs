using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MushroomPocket.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Effect",
                table: "Items",
                newName: "EffectValue");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EffectType",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EffectType",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "EffectValue",
                table: "Items",
                newName: "Effect");
        }
    }
}
