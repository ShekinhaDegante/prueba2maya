using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using prueba2maya.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using prueba2maya.Models;

namespace PracticaBiblioteca.Controllers
{
    // Asegura que los usuarios autenticados puedan acceder a este controlador
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acción para la vista de bienvenida, accesible para todos
        [AllowAnonymous]
        public IActionResult Welcome()
        {
            return View();
        }

        // Acción para la vista de inicio, con lógica para diferenciar roles
        public IActionResult Index()
        {
            // Verificamos el rol del usuario autenticado
            var userRole = User.IsInRole("Administrador") ? "Administrador" :
                           User.IsInRole("Cliente") ? "Cliente" : "Desconocido";

            // Pasamos el rol del usuario a la vista
            ViewData["UserRole"] = userRole;

            // Dependiendo del rol, podrías redirigir a diferentes vistas o mostrar contenido diferente
            if (userRole == "Administrador")
            {
                // Redirigir al dashboard de administrador o algo específico para el admin
                return RedirectToAction("AdminDashboard", "Admin");
            }
            else if (userRole == "Cliente")
            {
                // Redirigir al dashboard del cliente o algo específico para el cliente
                return RedirectToAction("UserDashboard", "User");
            }

            // Si el rol es desconocido, mostrar la página de inicio estándar
            return View();
        }

        // Acción para la vista de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción para manejar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
