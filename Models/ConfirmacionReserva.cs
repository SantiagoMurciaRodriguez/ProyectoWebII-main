using System.ComponentModel.DataAnnotations;

namespace ProyectoAerolineaWeb.Models
{
    public class ConfirmacionReserva
    {
        public int Id { get; set; }
        public int ServicioId { get; set; }

        [Required(ErrorMessage = "El contacto de emergencia es obligatorio.")]
        [Phone(ErrorMessage = "El número no es válido.")]
        [MinLength(10, ErrorMessage = "El número debe tener al menos 10 dígitos.")]
        [Display(Name = "Contacto de Emergencia")]
        [RegularExpression(@"^\d{10,}$", ErrorMessage = "Minimo 10 digitos.")]
        public string ContactoEmergencia { get; set; }
    }
}