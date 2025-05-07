namespace ProyectoAerolineaWeb.Models
{
    public class VuelosViewModel
    {
        public List<Vuelo> Disponibles { get; set; } = new List<Vuelo>();
        public List<Vuelo> NoDisponibles { get; set; } = new List<Vuelo>();
    }
}
