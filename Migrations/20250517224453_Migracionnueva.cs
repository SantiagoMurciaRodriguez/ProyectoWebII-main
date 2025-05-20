using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAerolineaWeb.Migrations
{
    /// <inheritdoc />
    public partial class Migracionnueva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPasajero_ConfirmacionesReserva_ConfirmacionReservaId",
                table: "DetallesPasajero");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPasajero_ConfirmacionReservaId",
                table: "DetallesPasajero");

            migrationBuilder.DropColumn(
                name: "ConfirmacionReservaId",
                table: "DetallesPasajero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfirmacionReservaId",
                table: "DetallesPasajero",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPasajero_ConfirmacionReservaId",
                table: "DetallesPasajero",
                column: "ConfirmacionReservaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPasajero_ConfirmacionesReserva_ConfirmacionReservaId",
                table: "DetallesPasajero",
                column: "ConfirmacionReservaId",
                principalTable: "ConfirmacionesReserva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
