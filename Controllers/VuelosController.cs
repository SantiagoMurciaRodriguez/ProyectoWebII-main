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
                return View(model);
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

            TempData["SuccessMessage"] = "Registro exitoso de pasajeros.";
            return RedirectToAction("Index", "Tarifas", new { VueloId = model.VueloId, PasajerosId = newPasajeros.Id });
        }
    }
}