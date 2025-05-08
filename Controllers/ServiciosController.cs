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

        public IActionResult Index()
        {
            var viewModel = new Servicios();
            return View("~/Views/Vuelos/Servicios.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult ServiciosPost(Servicios model)
        {
            if (ModelState.IsValid)
            {
                // Guardar los servicios adicionales en la base de datos
                var newServicio = new Servicios
                {
                    PasajerosId = model.PasajerosId,
                    Maletas = model.Maletas,
                    Comidas = model.Comidas,
                    Mascotas = model.Mascotas
                };

                _context.Servicios.Add(newServicio);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Servicios adicionales registrados exitosamente.";
                return RedirectToAction("Index", "Home"); // Redirigir a la página principal o donde desees
            }

            return View(model);
        }
    }
}
