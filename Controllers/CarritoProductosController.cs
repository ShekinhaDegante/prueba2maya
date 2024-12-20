using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: CarritoProductos
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.CarritoProductos.Include(c => c.IdCarritoNavigation).Include(c => c.IdProductoNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: CarritoProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarritoProductos == null)
            {
                return NotFound();
            }

            var carritoProducto = await _context.CarritoProductos
                .Include(c => c.IdCarritoNavigation)
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdCarritoProducto == id);
            if (carritoProducto == null)
            {
                return NotFound();
            }

            return View(carritoProducto);
        }

        // GET: CarritoProductos/Create
        public IActionResult Create()
        {
            ViewData["IdCarrito"] = new SelectList(_context.CarritoVentas, "IdCarrito", "IdCarrito");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: CarritoProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarritoProducto,IdCarrito,IdProducto,Cantidad")] CarritoProducto carritoProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritoProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCarrito"] = new SelectList(_context.CarritoVentas, "IdCarrito", "IdCarrito", carritoProducto.IdCarrito);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", carritoProducto.IdProducto);
            return View(carritoProducto);
        }

        // GET: CarritoProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarritoProductos == null)
            {
                return NotFound();
            }

            var carritoProducto = await _context.CarritoProductos.FindAsync(id);
            if (carritoProducto == null)
            {
                return NotFound();
            }
            ViewData["IdCarrito"] = new SelectList(_context.CarritoVentas, "IdCarrito", "IdCarrito", carritoProducto.IdCarrito);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", carritoProducto.IdProducto);
            return View(carritoProducto);
        }

        // POST: CarritoProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarritoProducto,IdCarrito,IdProducto,Cantidad")] CarritoProducto carritoProducto)
        {
            if (id != carritoProducto.IdCarritoProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoProductoExists(carritoProducto.IdCarritoProducto))
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
            ViewData["IdCarrito"] = new SelectList(_context.CarritoVentas, "IdCarrito", "IdCarrito", carritoProducto.IdCarrito);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", carritoProducto.IdProducto);
            return View(carritoProducto);
        }

        // GET: CarritoProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarritoProductos == null)
            {
                return NotFound();
            }

            var carritoProducto = await _context.CarritoProductos
                .Include(c => c.IdCarritoNavigation)
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdCarritoProducto == id);
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
            if (_context.CarritoProductos == null)
            {
                return Problem("Entity set 'TiendaContext.CarritoProductos'  is null.");
            }
            var carritoProducto = await _context.CarritoProductos.FindAsync(id);
            if (carritoProducto != null)
            {
                _context.CarritoProductos.Remove(carritoProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoProductoExists(int id)
        {
          return (_context.CarritoProductos?.Any(e => e.IdCarritoProducto == id)).GetValueOrDefault();
        }
    }
}
