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
    public class InventarioSucursalController : Controller
    {
        private readonly TiendaContext _context;

        public InventarioSucursalController(TiendaContext context)
        {
            _context = context;
        }

        // GET: InventarioSucursal
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.InventarioSucursals.Include(i => i.IdProductoNavigation).Include(i => i.IdSucursalNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: InventarioSucursal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InventarioSucursals == null)
            {
                return NotFound();
            }

            var inventarioSucursal = await _context.InventarioSucursals
                .Include(i => i.IdProductoNavigation)
                .Include(i => i.IdSucursalNavigation)
                .FirstOrDefaultAsync(m => m.IdInventario == id);
            if (inventarioSucursal == null)
            {
                return NotFound();
            }

            return View(inventarioSucursal);
        }

        // GET: InventarioSucursal/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal");
            return View();
        }

        // POST: InventarioSucursal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInventario,IdProducto,IdSucursal,Cantidad,FechaActualizacion")] InventarioSucursal inventarioSucursal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventarioSucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", inventarioSucursal.IdProducto);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", inventarioSucursal.IdSucursal);
            return View(inventarioSucursal);
        }

        // GET: InventarioSucursal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InventarioSucursals == null)
            {
                return NotFound();
            }

            var inventarioSucursal = await _context.InventarioSucursals.FindAsync(id);
            if (inventarioSucursal == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", inventarioSucursal.IdProducto);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", inventarioSucursal.IdSucursal);
            return View(inventarioSucursal);
        }

        // POST: InventarioSucursal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInventario,IdProducto,IdSucursal,Cantidad,FechaActualizacion")] InventarioSucursal inventarioSucursal)
        {
            if (id != inventarioSucursal.IdInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventarioSucursal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioSucursalExists(inventarioSucursal.IdInventario))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", inventarioSucursal.IdProducto);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", inventarioSucursal.IdSucursal);
            return View(inventarioSucursal);
        }

        // GET: InventarioSucursal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InventarioSucursals == null)
            {
                return NotFound();
            }

            var inventarioSucursal = await _context.InventarioSucursals
                .Include(i => i.IdProductoNavigation)
                .Include(i => i.IdSucursalNavigation)
                .FirstOrDefaultAsync(m => m.IdInventario == id);
            if (inventarioSucursal == null)
            {
                return NotFound();
            }

            return View(inventarioSucursal);
        }

        // POST: InventarioSucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InventarioSucursals == null)
            {
                return Problem("Entity set 'TiendaContext.InventarioSucursals'  is null.");
            }
            var inventarioSucursal = await _context.InventarioSucursals.FindAsync(id);
            if (inventarioSucursal != null)
            {
                _context.InventarioSucursals.Remove(inventarioSucursal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioSucursalExists(int id)
        {
          return (_context.InventarioSucursals?.Any(e => e.IdInventario == id)).GetValueOrDefault();
        }
    }
}
