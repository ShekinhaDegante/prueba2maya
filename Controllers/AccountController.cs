using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prueba2maya.Models;
using prueba2maya.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace prueba2maya.Controllers
{
    public class AccountController : Controller
    {
        private readonly TiendaContext _context;

        public AccountController(TiendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Usuarios
                    .Include(u => u.IdRolNavigation) // Cargar la relación con Role
                    .SingleOrDefaultAsync(u => u.NombreUsuario == model.NombreUsuario && u.Contrasena == model.Contrasena);

                if (user != null)
                {
                    // Crear los claims del usuario
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.NombreUsuario),
                        new Claim(ClaimTypes.Role, user.IdRolNavigation.NombreRol) // Acceder a la propiedad del rol
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Autenticación del usuario
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Redirigir basado en el rol del usuario
                    if (user.IdRolNavigation.NombreRol == "Administrador")
                    {
                        return RedirectToAction("Index", "Home"); // Acción para Administrador
                    }
                    else if (user.IdRolNavigation.NombreRol == "Cliente")
                    {
                        return RedirectToAction("Index", "Home"); // Acción para Cliente
                    }
                }

                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            // Filtrar la lista de roles para incluir solo el rol "Cliente" (IdRol = 3)
            var roles = _context.Roles.Where(r => r.IdRol == 3).ToList();

            // Crear un SelectList con los roles filtrados
            ViewBag.Roles = new SelectList(roles, "IdRol", "NombreRol");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo electrónico ya existe en la base de datos
                var existingUser = await _context.Usuarios
                    .SingleOrDefaultAsync(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    // Si ya existe un usuario con ese correo, agregar un error de modelo
                    ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");

                    // Solo pasar el rol de Cliente (IdRol = 3)
                    ViewBag.Roles = new SelectList(_context.Roles.Where(r => r.IdRol == 3), "IdRol", "NombreRol");
                    return View(model);  // Regresar a la vista con el error
                }

                // Crear un nuevo usuario si el correo no está registrado
                var usuario = new Usuario
                {
                    NombreUsuario = model.NombreUsuario,
                    Contrasena = model.Contrasena,
                    Email = model.Email,
                    IdRol = model.IdRol,
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                // Redirigir a la vista Welcome después del registro
                return RedirectToAction("Welcome", "Home");  // Cambiar "Home" por el controlador que maneja la vista de bienvenida
            }

            // Si no es válido, filtrar solo el rol de Cliente (IdRol = 3)
            ViewBag.Roles = new SelectList(_context.Roles.Where(r => r.IdRol == 3), "IdRol", "NombreRol");
            return View(model);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
