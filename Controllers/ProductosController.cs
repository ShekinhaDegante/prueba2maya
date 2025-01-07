using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prueba2maya.Models;

namespace prueba2maya.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaContext _context;

        public ProductosController(TiendaContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.Productos.Include(p => p.IdCategoriaNavigation).Include(p => p.IdProveedorNavigation).Include(p => p.IdSucursalNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.IdSucursalNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasProductos, "IdCategoria", "NombreCategoria");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor");
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal");
            return View();
        }

        // POST: Productos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile Imagen, [Bind("IdProducto,NombreProducto,Descripcion,Precio,CantidadDisponible,IdCategoria,IdSucursal,IdProveedor,ImagenUrl")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (Imagen != null && Imagen.Length > 0)
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                    var fileExt = Path.GetExtension(Imagen.FileName).Substring(1).ToLower();

                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("Imagen", "Solo se permiten archivos de imagen (jpg, jpeg, png, gif).");
                        ViewData["IdCategoria"] = new SelectList(_context.CategoriasProductos, "IdCategoria", "IdCategoria", producto.IdCategoria);
                        ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", producto.IdProveedor);
                        ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", producto.IdSucursal);
                        return View(producto);
                    }

                    if (Imagen.Length > 2 * 1024 * 1024) // Limitar a 2MB
                    {
                        ModelState.AddModelError("Imagen", "El archivo es demasiado grande. Máximo 2MB.");
                        ViewData["IdCategoria"] = new SelectList(_context.CategoriasProductos, "IdCategoria", "IdCategoria", producto.IdCategoria);
                        ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", producto.IdProveedor);
                        ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", producto.IdSucursal);
                        return View(producto);
                    }

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    Directory.CreateDirectory(uploadsFolder); // Asegurarse de que la carpeta exista

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Imagen.CopyToAsync(fileStream);
                    }

                    producto.ImagenUrl = "/images/" + uniqueFileName;
                }

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCategoria"] = new SelectList(_context.CategoriasProductos, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", producto.IdProveedor);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", producto.IdSucursal);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriasProductos, "IdCategoria", "NombreCategoria", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", producto.IdProveedor);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal", producto.IdSucursal);
            return View(producto);
        }

        // POST: Productos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile Imagen, [Bind("IdProducto,NombreProducto,Descripcion,Precio,CantidadDisponible,IdCategoria,IdSucursal,IdProveedor,ImagenUrl")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar si se sube una nueva imagen
                    if (Imagen != null && Imagen.Length > 0)
                    {
                        var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                        var fileExt = Path.GetExtension(Imagen.FileName).Substring(1).ToLower();

                        if (!supportedTypes.Contains(fileExt))
                        {
                            ModelState.AddModelError("Imagen", "Solo se permiten archivos de imagen (jpg, jpeg, png, gif).");
                            return View(producto);
                        }

                        if (Imagen.Length > 2 * 1024 * 1024) // Limitar a 2MB
                        {
                            ModelState.AddModelError("Imagen", "El archivo es demasiado grande. Máximo 2MB.");
                            return View(producto);
                        }

                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await Imagen.CopyToAsync(fileStream);
                        }

                        producto.ImagenUrl = "/images/" + uniqueFileName;
                    }

                    // Actualizar los detalles del producto en la base de datos
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCategoria"] = new SelectList(_context.CategoriasProductos, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", producto.IdProveedor);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", producto.IdSucursal);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.IdSucursalNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'TiendaContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Método para verificar si un producto existe
        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }

        // Métodos Delete y Existentes no requieren cambios.
    }
}
