using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMDTServerModule.Migrations
{
    /// <inheritdoc />
    public partial class newpropertiestoprinterstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "brand_name",
                table: "Printers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "table_X",
                table: "Printers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "table_Y",
                table: "Printers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brand_name",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "table_X",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "table_Y",
                table: "Printers");
        }
    }
}
