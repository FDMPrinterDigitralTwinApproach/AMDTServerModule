using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMDTServerModule.Migrations
{
    /// <inheritdoc />
    public partial class printers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    aktif = table.Column<bool>(type: "bit", nullable: false),
                    createdat = table.Column<DateTime>(name: "created-at", type: "datetime2", nullable: false),
                    created_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Printers_PrinterFarms_farm_id",
                        column: x => x.farm_id,
                        principalTable: "PrinterFarms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Printers_farm_id",
                table: "Printers",
                column: "farm_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Printers");
        }
    }
}
