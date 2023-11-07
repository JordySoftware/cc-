using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AseguradoraViamatica.Migrations
{
    /// <inheritdoc />
    public partial class Seguros_Asegurados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SegurosAsegurados",
                columns: table => new
                {
                    AseguradoId = table.Column<int>(type: "int", nullable: false),
                    SeguroId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegurosAsegurados", x => new { x.SeguroId, x.AseguradoId });
                    table.ForeignKey(
                        name: "FK_SegurosAsegurados_Asegurados_AseguradoId",
                        column: x => x.AseguradoId,
                        principalTable: "Asegurados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SegurosAsegurados_Seguros_SeguroId",
                        column: x => x.SeguroId,
                        principalTable: "Seguros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SegurosAsegurados_AseguradoId",
                table: "SegurosAsegurados",
                column: "AseguradoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SegurosAsegurados");
        }
    }
}
