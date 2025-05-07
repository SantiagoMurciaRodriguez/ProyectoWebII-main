namespace ProyectoAerolineaWeb.Models
{
    public class Tarifa
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public decimal Precio { get; set; }
        public int VueloId { get; set; }
        public Vuelo Vuelo { get; set; } = default!;
    }
}


