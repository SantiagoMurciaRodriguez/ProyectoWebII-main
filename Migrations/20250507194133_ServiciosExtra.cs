using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAerolineaWeb.Migrations
{
    /// <inheritdoc />
    public partial class ServiciosExtra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasajerosId = table.Column<int>(type: "int", nullable: false),
                    Maletas = table.Column<int>(type: "int", nullable: false),
                    Comidas = table.Column<int>(type: "int", nullable: false),
                    Mascotas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servicios");
        }
    }
}
