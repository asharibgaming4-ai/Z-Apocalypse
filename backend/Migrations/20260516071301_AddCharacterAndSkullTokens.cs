using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectaaa.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterAndSkullTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Accuracy",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Armor",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMedeaUnlocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SelectedCharacter",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SkullTokens",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accuracy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Armor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsMedeaUnlocked",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SelectedCharacter",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SkullTokens",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Users");
        }
    }
}
