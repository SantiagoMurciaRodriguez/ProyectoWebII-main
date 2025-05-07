using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAerolineaWeb.Data;
using ProyectoAerolineaWeb.Models;

namespace ProyectoAerolineaWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new Login();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                // Buscar al usuario en la base de datos
                var user = _context.Users
                    .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Guardar datos del usuario en la sesión
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    TempData["SuccessMessage"] = $"¡Bienvenido, {user.Name}!";
                    return RedirectToAction("Index", "Home"); // Redirigir a la página principal
                }
                else
                {
                    TempData["ErrorMessage"] = "Credenciales incorrectas.";
                }
            }

            // Si el modelo no es válido o las credenciales son incorrectas, volver a mostrar la vista
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new User();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya está registrado
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    TempData["ErrorMessage"] = "El correo electrónico ya está registrado.";
                    return View(model);
                }

                // Crear un nuevo usuario
                var newUser = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password // Nota: Considera encriptar la contraseña
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Registro exitoso. Ahora puedes iniciar sesión.";
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Logout()
        {
            // Limpiar todos los datos de la sesión
            HttpContext.Session.Clear();

            // Redirigir al login o a la página principal
            return RedirectToAction("Index", "Login");
        }
    }
}
