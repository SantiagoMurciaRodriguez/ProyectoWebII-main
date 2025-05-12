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
                    VueloId = model.VueloId, // Asignar VueloId
                    TarifaId = model.TarifaId, // Asignar TarifaId
                    Maletas = model.Maletas,
                    Comidas = model.Comidas,
                    Mascotas = model.Mascotas
                };

                _context.Servicios.Add(newServicio);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Servicios adicionales registrados exitosamente.";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
