using Microsoft.AspNetCore.Mvc;
using ProyectoAerolineaWeb.Data;
using ProyectoAerolineaWeb.Models;
using ProyectoAerolineaWeb.Views.Vuelos;

namespace ProyectoAerolineaWeb.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int tarifaId, int vueloId, int pasajerosId)
        {
            var viewModel = new Servicios
            {
                PasajerosId = pasajerosId
            };

            ViewBag.TarifaId = tarifaId;
            ViewBag.VueloId = vueloId;

            return View("~/Views/Vuelos/Servicios.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult ServiciosPost(Servicios model)
        {
            if (ModelState.IsValid)
            {
                var newServicio = new Servicios
                {
                    PasajerosId = model.PasajerosId,
                    VueloId = model.VueloId,
                    TarifaId = model.TarifaId,
                    Maletas = model.Maletas,
                    Comidas = model.Comidas,
                    Mascotas = model.Mascotas
                };

                _context.Servicios.Add(newServicio);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Servicios adicionales registrados exitosamente.";
                return RedirectToAction("Confirmar", new { servicioId = newServicio.Id });
            }

            return View(model);
        }
        // GET: Confirmar
        public IActionResult Confirmar(int servicioId)
        {
            var servicio = _context.Servicios.FirstOrDefault(s => s.Id == servicioId);
            if (servicio == null) return NotFound();

            ViewBag.Servicio = servicio;
            var model = new ConfirmacionReserva { ServicioId = servicioId };
            return View("~/Views/Vuelos/Confirmar.cshtml", model); // Ruta explícita
        }

        [HttpPost]
        public IActionResult Confirmar(ConfirmacionReserva model)
        {
            if (ModelState.IsValid)
            {
                _context.ConfirmacionesReserva.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Reserva confirmada correctamente.";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Servicio = _context.Servicios.FirstOrDefault(s => s.Id == model.ServicioId);
            return View("~/Views/Vuelos/Confirmar.cshtml", model); // Ruta explícita
        }
        [HttpPost]
        public IActionResult Cancelar(int servicioId)
        {
            // Eliminar confirmación si existe
            var confirmacion = _context.ConfirmacionesReserva.FirstOrDefault(c => c.ServicioId == servicioId);
            if (confirmacion != null)
            {
                _context.ConfirmacionesReserva.Remove(confirmacion);
            }

            // Eliminar servicio y pasajero relacionado
            var servicio = _context.Servicios.FirstOrDefault(s => s.Id == servicioId);
            if (servicio != null)
            {
                // Eliminar pasajero relacionado
                var pasajero = _context.Pasajeros.FirstOrDefault(p => p.Id == servicio.PasajerosId);
                if (pasajero != null)
                {
                    _context.Pasajeros.Remove(pasajero);
                }

                _context.Servicios.Remove(servicio);
            }

            _context.SaveChanges();
            TempData["SuccessMessage"] = "Reserva cancelada correctamente.";
            return RedirectToAction("Index", "Home");
        }
    }
}
