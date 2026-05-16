using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectaaa.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSettingsStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FullscreenMode",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GraphicsQuality",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MasterVolume",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MusicVolume",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RenderScale",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SfxVolume",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullscreenMode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GraphicsQuality",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MasterVolume",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MusicVolume",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RenderScale",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SfxVolume",
                table: "Users");
        }
    }
}
