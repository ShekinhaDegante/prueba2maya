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
            var idUsuario = 1; // Reemplázalo con el valor real de usuario logueado

            // Buscar el carrito del usuario
            var carrito = await _context.CarritoVentas
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

            if (carrito == null)
            {
                // Si no existe un carrito, mostrar mensaje
                ViewBag.Message = "Tu carrito está vacío.";
                return View();
            }

            // Obtener los productos del carrito
            var productosCarrito = await _context.CarritoProductos
                .Where(cp => cp.IdCarrito == carrito.IdCarrito)
                .Include(cp => cp.IdProductoNavigation)
                .ToListAsync();

            return View(productosCarrito);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int idProducto)
        {
            var idUsuario = 1; // Obtén el ID del usuario logueado dinámicamente.

            // Verificar si el usuario tiene un carrito
            var carrito = await _context.CarritoVentas.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

            if (carrito == null)
            {
                carrito = new CarritoVenta
                {
                    IdUsuario = idUsuario,
                    FechaCreacion = DateTime.Now
                };
                _context.CarritoVentas.Add(carrito);
                await _context.SaveChangesAsync();
            }

            // Verificar si el producto ya está en el carrito
            var carritoProducto = await _context.CarritoProductos
                .FirstOrDefaultAsync(cp => cp.IdCarrito == carrito.IdCarrito && cp.IdProducto == idProducto);

            if (carritoProducto != null)
            {
                carritoProducto.Cantidad++;
                _context.Update(carritoProducto);
            }
            else
            {
                carritoProducto = new CarritoProducto
                {
                    IdCarrito = carrito.IdCarrito,
                    IdProducto = idProducto,
                    Cantidad = 1
                };
                _context.CarritoProductos.Add(carritoProducto);
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Producto agregado al carrito exitosamente." });
        }

        // GET: CarritoProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoProducto = await _context.CarritoProductos
                .Include(cp => cp.IdCarritoNavigation)
                .Include(cp => cp.IdProductoNavigation)
                .FirstOrDefaultAsync(cp => cp.IdCarritoProducto == id);

            if (carritoProducto == null)
            {
                return NotFound();
            }

            return View(carritoProducto);
        }

        // POST: CarritoProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carritoProducto = await _context.CarritoProductos.FindAsync(id);

            if (carritoProducto == null)
            {
                return NotFound();
            }

            _context.CarritoProductos.Remove(carritoProducto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MiCarrito));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
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
