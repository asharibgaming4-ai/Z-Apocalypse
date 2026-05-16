using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectaaa.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxHealth",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementSpeed",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SelectedSkin",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stamina",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxHealth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MovementSpeed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SelectedSkin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Stamina",
                table: "Users");
        }
    }
}
