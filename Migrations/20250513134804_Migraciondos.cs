using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAerolineaWeb.Migrations
{
    /// <inheritdoc />
    public partial class Migraciondos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfirmacionesReserva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicioId = table.Column<int>(type: "int", nullable: false),
                    ContactoEmergencia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmacionesReserva", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmacionesReserva");
        }
    }
}
