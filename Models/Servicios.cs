﻿namespace ProyectoAerolineaWeb.Models
{
    public class Servicios
    {
        public int Id { get; set; }
        public int PasajerosId { get; set; }
        public int VueloId { get; set; }
        public int TarifaId { get; set; }
        public int Maletas { get; set; }
        public int Comidas { get; set; }
        public int Mascotas { get; set; }
    }
}