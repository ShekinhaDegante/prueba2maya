using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prueba2maya.Controllers
{
    // Asegúrate de que solo los usuarios con el rol 'Administrador' puedan acceder a este controlador
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        // Acción que muestra el Dashboard del Administrador
        public IActionResult AdminDashboard()
        {
            return View();
        }


        // Otros métodos y acciones para el Administrador, por ejemplo:
        public IActionResult ManageUsers()
        {
            // Lógica para gestionar usuarios
            return View();
        }

        public IActionResult ViewOrders()
        {
            // Lógica para ver pedidos
            return View();
        }

        // Puedes agregar más acciones que solo el administrador podrá ver
    }
}
