using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba2maya.Models;

namespace prueba2maya.Controllers
{
    public class CarritoProductosController : Controller
    {
        private readonly TiendaContext _context;

        public CarritoProductosController(TiendaContext context)
        {
            _context = context;
        }

        // GET: MiCarrito
        public async Task<IActionResult> MiCarrito()
        {
            // Obtener el IdUsuario del usuario logueado
            var idUsuario = 1; // Reemplázalo con el valor real de usuario logueado

            // Buscar el carrito del usuario
            var carrito = await _context.CarritoVentas
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

            if (carrito == null)
            {
                // Si no existe un carrito, se puede mostrar un mensaje
                ViewBag.Message = "Tu carrito está vacío.";
                return View();
            }

            // Obtener los productos del carrito
            var productosCarrito = await _context.CarritoProductos
                .Where(cp => cp.IdCarrito == carrito.IdCarrito)
                .Include(cp => cp.IdProductoNavigation) // Asumiendo que tienes una relación de navegación para Producto
                .ToListAsync();

            return View(productosCarrito);
        }

        // POST: CarritoVenta/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int idProducto)
        {
            // Obtener el IdUsuario del usuario logueado
            var idUsuario = 1;  // Reemplázalo con el valor real de usuario logueado

            // Verificar si el usuario tiene un carrito
            var carrito = await _context.CarritoVentas
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

            // Si no tiene carrito, crea uno nuevo
            if (carrito == null)
            {
                carrito = new CarritoVenta
                {
                    IdUsuario = idUsuario,
                    FechaCreacion = DateTime.Now
                };
                _context.CarritoVentas.Add(carrito);
                await _context.SaveChangesAsync(); // Guardar carrito recién creado
            }

            // Verificar si el producto ya está en el carrito
            var carritoProducto = await _context.CarritoProductos
                .FirstOrDefaultAsync(cp => cp.IdCarrito == carrito.IdCarrito && cp.IdProducto == idProducto);

            if (carritoProducto != null)
            {
                // Si el producto ya está en el carrito, aumentar la cantidad
                carritoProducto.Cantidad++;
                _context.Update(carritoProducto);
            }
            else
            {
                // Si no existe, agregarlo
                carritoProducto = new CarritoProducto
                {
                    IdCarrito = carrito.IdCarrito,
                    IdProducto = idProducto,
                    Cantidad = 1
                };
                _context.CarritoProductos.Add(carritoProducto);
            }

            await _context.SaveChangesAsync(); // Guardar los cambios

            return RedirectToAction(nameof(MiCarrito)); // Redirigir a la vista del carrito
        }

        // POST: EliminarProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            // ID del carrito asociado al usuario actual
            var idUsuario = 1; // Reemplázalo con el valor real de usuario logueado

            // Buscar el carrito del usuario
            var carrito = await _context.CarritoVentas
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

            if (carrito == null)
            {
                return NotFound();
            }

            // Buscar el producto en el carrito
            var carritoProducto = await _context.CarritoProductos
                .FirstOrDefaultAsync(cp => cp.IdCarrito == carrito.IdCarrito && cp.IdProducto == idProducto);

            if (carritoProducto == null)
            {
                return NotFound();
            }

            _context.CarritoProductos.Remove(carritoProducto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MiCarrito)); // Redirigir a la vista del carrito
        }
    }
}
