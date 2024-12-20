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
    public class HistorialPreciosController : Controller
    {
        private readonly TiendaContext _context;

        public HistorialPreciosController(TiendaContext context)
        {
            _context = context;
        }

        // GET: HistorialPrecios
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.HistorialPrecios.Include(h => h.IdProductoNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: HistorialPrecios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HistorialPrecios == null)
            {
                return NotFound();
            }

            var historialPrecio = await _context.HistorialPrecios
                .Include(h => h.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdHistorial == id);
            if (historialPrecio == null)
            {
                return NotFound();
            }

            return View(historialPrecio);
        }

        // GET: HistorialPrecios/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: HistorialPrecios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHistorial,IdProducto,PrecioAnterior,PrecioNuevo,FechaCambio")] HistorialPrecio historialPrecio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialPrecio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", historialPrecio.IdProducto);
            return View(historialPrecio);
        }

        // GET: HistorialPrecios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HistorialPrecios == null)
            {
                return NotFound();
            }

            var historialPrecio = await _context.HistorialPrecios.FindAsync(id);
            if (historialPrecio == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", historialPrecio.IdProducto);
            return View(historialPrecio);
        }

        // POST: HistorialPrecios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHistorial,IdProducto,PrecioAnterior,PrecioNuevo,FechaCambio")] HistorialPrecio historialPrecio)
        {
            if (id != historialPrecio.IdHistorial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialPrecio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialPrecioExists(historialPrecio.IdHistorial))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", historialPrecio.IdProducto);
            return View(historialPrecio);
        }

        // GET: HistorialPrecios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HistorialPrecios == null)
            {
                return NotFound();
            }

            var historialPrecio = await _context.HistorialPrecios
                .Include(h => h.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdHistorial == id);
            if (historialPrecio == null)
            {
                return NotFound();
            }

            return View(historialPrecio);
        }

        // POST: HistorialPrecios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HistorialPrecios == null)
            {
                return Problem("Entity set 'TiendaContext.HistorialPrecios'  is null.");
            }
            var historialPrecio = await _context.HistorialPrecios.FindAsync(id);
            if (historialPrecio != null)
            {
                _context.HistorialPrecios.Remove(historialPrecio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialPrecioExists(int id)
        {
          return (_context.HistorialPrecios?.Any(e => e.IdHistorial == id)).GetValueOrDefault();
        }
    }
}
