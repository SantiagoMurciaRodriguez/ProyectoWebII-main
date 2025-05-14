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

            var vuelo = _context.Vuelos.FirstOrDefault(v => v.Id == VueloId);
            ViewBag.AsientosDisponibles = vuelo?.AsientosDisponibles ?? 0;

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

            // Calcular el total de pasajeros a registrar
            int totalPasajeros = model.Ancianos + model.Adultos + model.Niños;

            // Buscar el vuelo correspondiente
            var vuelo = _context.Vuelos.FirstOrDefault(v => v.Id == model.VueloId);
            if (vuelo == null)
            {
                TempData["ErrorMessage"] = "Vuelo no encontrado.";
                return View(model);
            }

            // Validar stock de asientos
            if (totalPasajeros > vuelo.AsientosDisponibles)
            {
                TempData["ErrorMessage"] = $"No hay suficientes asientos disponibles. Solo quedan {vuelo.AsientosDisponibles}.";
                return View(model);
            }

            // Descontar asientos
            vuelo.AsientosDisponibles -= totalPasajeros;
            _context.SaveChanges();

            // Registrar los pasajeros
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