namespace ProyectoAerolineaWeb.Models
{
    public class Vuelo
    {
        public int Id { get; set; }
        public string NumeroVuelo { get; set; } = string.Empty;

        public int CiudadOrigenId { get; set; }
        public Ciudad CiudadOrigen { get; set; } = default!;

        public int CiudadDestinoId { get; set; }
        public Ciudad CiudadDestino { get; set; } = default!;

        public DateTime Fecha { get; set; }
        public int AsientosDisponibles { get; set; }
    }
}
