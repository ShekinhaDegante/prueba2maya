using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prueba2maya.Controllers
{
    // Asegúrate de que solo los usuarios con el rol 'Cliente' puedan acceder a este controlador
    [Authorize(Roles = "Cliente")]
    public class UserController : Controller
    {
        // Acción que muestra el Dashboard del Cliente
        public IActionResult UserDashboard()
        {
            return View();
        }

        // Otros métodos y acciones para el Usuario (Cliente)
        public IActionResult ViewProfile()
        {
            // Lógica para mostrar el perfil del cliente
            return View();
        }

        public IActionResult ViewOrders()
        {
            // Lógica para ver los pedidos del cliente
            return View();
        }

        // Puedes agregar más acciones que solo el cliente podrá ver
    }
}
