namespace ProyectoAerolineaWeb.Models
{
    public class ConfirmacionReserva
    {
        public int Id { get; set; }
        public int ServicioId { get; set; }
        public string ContactoEmergencia { get; set; } = string.Empty;
    }
}