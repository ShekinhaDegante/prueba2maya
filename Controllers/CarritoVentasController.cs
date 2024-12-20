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
    public class CarritoVentasController : Controller
    {
        private readonly TiendaContext _context;

        public CarritoVentasController(TiendaContext context)
        {
            _context = context;
        }

        // GET: CarritoVentas
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.CarritoVentas.Include(c => c.IdUsuarioNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: CarritoVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarritoVentas == null)
            {
                return NotFound();
            }

            var carritoVenta = await _context.CarritoVentas
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCarrito == id);
            if (carritoVenta == null)
            {
                return NotFound();
            }

            return View(carritoVenta);
        }

        // GET: CarritoVentas/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: CarritoVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarrito,IdUsuario,FechaCreacion")] CarritoVenta carritoVenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritoVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", carritoVenta.IdUsuario);
            return View(carritoVenta);
        }

        // GET: CarritoVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarritoVentas == null)
            {
                return NotFound();
            }

            var carritoVenta = await _context.CarritoVentas.FindAsync(id);
            if (carritoVenta == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", carritoVenta.IdUsuario);
            return View(carritoVenta);
        }

        // POST: CarritoVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarrito,IdUsuario,FechaCreacion")] CarritoVenta carritoVenta)
        {
            if (id != carritoVenta.IdCarrito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoVentaExists(carritoVenta.IdCarrito))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", carritoVenta.IdUsuario);
            return View(carritoVenta);
        }

        // GET: CarritoVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarritoVentas == null)
            {
                return NotFound();
            }

            var carritoVenta = await _context.CarritoVentas
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCarrito == id);
            if (carritoVenta == null)
            {
                return NotFound();
            }

            return View(carritoVenta);
        }

        // POST: CarritoVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarritoVentas == null)
            {
                return Problem("Entity set 'TiendaContext.CarritoVentas'  is null.");
            }
            var carritoVenta = await _context.CarritoVentas.FindAsync(id);
            if (carritoVenta != null)
            {
                _context.CarritoVentas.Remove(carritoVenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoVentaExists(int id)
        {
          return (_context.CarritoVentas?.Any(e => e.IdCarrito == id)).GetValueOrDefault();
        }
    }
}
