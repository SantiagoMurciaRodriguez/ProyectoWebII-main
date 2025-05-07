using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAerolineaWeb.Data;
using ProyectoAerolineaWeb.Models;
using System.Threading.Tasks;

namespace ProyectoAerolineaWeb.Controllers
{
    public class VuelosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VuelosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var disponibles = await _context.Vuelos
                .Include(v => v.CiudadOrigen)
                .Include(v => v.CiudadDestino)
                .Where(v => v.AsientosDisponibles > 0)
                .ToListAsync();

            var noDisponibles = await _context.Vuelos
                .Include(v => v.CiudadOrigen)
                .Include(v => v.CiudadDestino)
                .Where(v => v.AsientosDisponibles == 0)
                .ToListAsync();

            var viewModel = new VuelosViewModel
            {
                Disponibles = disponibles,
                NoDisponibles = noDisponibles
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Cantidad(int VueloId)
        {
            var model = new Pasajeros
            {
                VueloId = VueloId,
                Ancianos = 0,
                Adultos = 0,
                Niños = 0
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Cantidad(Pasajeros model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "El modelo no es válido. Por favor, verifica los datos.";
                return View(model); // Asegúrate de devolver el modelo
            }

            var pasajeros = _context.Pasajeros
                .FirstOrDefault(p => p.Id == model.Id);

            if (pasajeros != null)
            {
                TempData["ErrorMessage"] = $"El vuelo {pasajeros.Id} ya se encuentra en la base de datos";
                return View(model); // Devuelve el modelo para evitar que sea null
            }

            var newPasajeros = new Pasajeros
            {
                VueloId = model.VueloId,
                Ancianos = model.Ancianos,
                Adultos = model.Adultos,
                Niños = model.Niños
            };

            _context.Pasajeros.Add(newPasajeros);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Registro exitoso de número de pasajeros.";
            return RedirectToAction("Index"); // Redirige a otra acción después del éxito
        }
    }
}





