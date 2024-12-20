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
    public class DetallePedidosProveedoresController : Controller
    {
        private readonly TiendaContext _context;

        public DetallePedidosProveedoresController(TiendaContext context)
        {
            _context = context;
        }

        // GET: DetallePedidosProveedores
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.DetallePedidosProveedores.Include(d => d.IdPedidoProveedorNavigation).Include(d => d.IdProductoNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: DetallePedidosProveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetallePedidosProveedores == null)
            {
                return NotFound();
            }

            var detallePedidosProveedore = await _context.DetallePedidosProveedores
                .Include(d => d.IdPedidoProveedorNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallePedidoProveedor == id);
            if (detallePedidosProveedore == null)
            {
                return NotFound();
            }

            return View(detallePedidosProveedore);
        }

        // GET: DetallePedidosProveedores/Create
        public IActionResult Create()
        {
            ViewData["IdPedidoProveedor"] = new SelectList(_context.PedidosProveedores, "IdPedidoProveedor", "IdPedidoProveedor");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: DetallePedidosProveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetallePedidoProveedor,IdPedidoProveedor,IdProducto,CantidadSolicitada,PrecioUnitario")] DetallePedidosProveedore detallePedidosProveedore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallePedidosProveedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedidoProveedor"] = new SelectList(_context.PedidosProveedores, "IdPedidoProveedor", "IdPedidoProveedor", detallePedidosProveedore.IdPedidoProveedor);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallePedidosProveedore.IdProducto);
            return View(detallePedidosProveedore);
        }

        // GET: DetallePedidosProveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetallePedidosProveedores == null)
            {
                return NotFound();
            }

            var detallePedidosProveedore = await _context.DetallePedidosProveedores.FindAsync(id);
            if (detallePedidosProveedore == null)
            {
                return NotFound();
            }
            ViewData["IdPedidoProveedor"] = new SelectList(_context.PedidosProveedores, "IdPedidoProveedor", "IdPedidoProveedor", detallePedidosProveedore.IdPedidoProveedor);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallePedidosProveedore.IdProducto);
            return View(detallePedidosProveedore);
        }

        // POST: DetallePedidosProveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetallePedidoProveedor,IdPedidoProveedor,IdProducto,CantidadSolicitada,PrecioUnitario")] DetallePedidosProveedore detallePedidosProveedore)
        {
            if (id != detallePedidosProveedore.IdDetallePedidoProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePedidosProveedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePedidosProveedoreExists(detallePedidosProveedore.IdDetallePedidoProveedor))
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
            ViewData["IdPedidoProveedor"] = new SelectList(_context.PedidosProveedores, "IdPedidoProveedor", "IdPedidoProveedor", detallePedidosProveedore.IdPedidoProveedor);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detallePedidosProveedore.IdProducto);
            return View(detallePedidosProveedore);
        }

        // GET: DetallePedidosProveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetallePedidosProveedores == null)
            {
                return NotFound();
            }

            var detallePedidosProveedore = await _context.DetallePedidosProveedores
                .Include(d => d.IdPedidoProveedorNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallePedidoProveedor == id);
            if (detallePedidosProveedore == null)
            {
                return NotFound();
            }

            return View(detallePedidosProveedore);
        }

        // POST: DetallePedidosProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetallePedidosProveedores == null)
            {
                return Problem("Entity set 'TiendaContext.DetallePedidosProveedores'  is null.");
            }
            var detallePedidosProveedore = await _context.DetallePedidosProveedores.FindAsync(id);
            if (detallePedidosProveedore != null)
            {
                _context.DetallePedidosProveedores.Remove(detallePedidosProveedore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallePedidosProveedoreExists(int id)
        {
          return (_context.DetallePedidosProveedores?.Any(e => e.IdDetallePedidoProveedor == id)).GetValueOrDefault();
        }
    }
}
