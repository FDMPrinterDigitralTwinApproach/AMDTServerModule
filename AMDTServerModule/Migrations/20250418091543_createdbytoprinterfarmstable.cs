using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMDTServerModule.Migrations
{
    /// <inheritdoc />
    public partial class createdbytoprinterfarmstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "PrinterFarms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "PrinterFarms");
        }
    }
}
