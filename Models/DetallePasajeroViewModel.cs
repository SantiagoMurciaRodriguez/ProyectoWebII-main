namespace ProyectoAerolineaWeb.Models
{
    public class DetallePasajeroViewModel
    {
        public int PasajerosId { get; set; }
        public List<DetallePasajero> Detalles { get; set; } = new();
    }
}
