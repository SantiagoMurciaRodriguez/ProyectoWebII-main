using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoAerolineaWeb.Models;

namespace ProyectoAerolineaWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Dashboard()
    {
        var userName = HttpContext.Session.GetString("UserName");
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (string.IsNullOrEmpty(userName))
        {
            return RedirectToAction("Index", "Login"); // Redirigir al login si no hay sesión
        }

        ViewBag.UserName = userName;
        ViewBag.UserEmail = userEmail;

        return View();
    }
}
