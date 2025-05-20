namespace ProyectoAerolineaWeb.Models
{
    public class DetallePasajero
    {
        public int Id { get; set; }
        public int PasajerosId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // "Anciano", "Adulto", "Niño"
    }
}
