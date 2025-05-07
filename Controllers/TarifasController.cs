using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAerolineaWeb.Data;
using ProyectoAerolineaWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAerolineaWeb.Controllers
{
    public class TarifasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TarifasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int VueloId)
        {
            var tarifas = await _context.Tarifas.Where(t => t.VueloId == VueloId).ToListAsync();
            return View(tarifas);
        }
    }
}