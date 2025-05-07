namespace ProyectoAerolineaWeb.Models
{
    public class Pasajeros
    {
        public int Id { get; set; }
        public int VueloId { get; set; }
        public int Ancianos { get; set; }
        public int Adultos { get; set; }
        public int Niños { get; set; }
    }
}