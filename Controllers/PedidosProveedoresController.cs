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
    public class PedidosProveedoresController : Controller
    {
        private readonly TiendaContext _context;

        public PedidosProveedoresController(TiendaContext context)
        {
            _context = context;
        }

        // GET: PedidosProveedores
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.PedidosProveedores.Include(p => p.IdProveedorNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: PedidosProveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PedidosProveedores == null)
            {
                return NotFound();
            }

            var pedidosProveedore = await _context.PedidosProveedores
                .Include(p => p.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.IdPedidoProveedor == id);
            if (pedidosProveedore == null)
            {
                return NotFound();
            }

            return View(pedidosProveedore);
        }

        // GET: PedidosProveedores/Create
        public IActionResult Create()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor");
            return View();
        }

        // POST: PedidosProveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedidoProveedor,IdProveedor,FechaPedido,Estado")] PedidosProveedore pedidosProveedore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidosProveedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", pedidosProveedore.IdProveedor);
            return View(pedidosProveedore);
        }

        // GET: PedidosProveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PedidosProveedores == null)
            {
                return NotFound();
            }

            var pedidosProveedore = await _context.PedidosProveedores.FindAsync(id);
            if (pedidosProveedore == null)
            {
                return NotFound();
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", pedidosProveedore.IdProveedor);
            return View(pedidosProveedore);
        }

        // POST: PedidosProveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedidoProveedor,IdProveedor,FechaPedido,Estado")] PedidosProveedore pedidosProveedore)
        {
            if (id != pedidosProveedore.IdPedidoProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidosProveedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidosProveedoreExists(pedidosProveedore.IdPedidoProveedor))
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
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", pedidosProveedore.IdProveedor);
            return View(pedidosProveedore);
        }

        // GET: PedidosProveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PedidosProveedores == null)
            {
                return NotFound();
            }

            var pedidosProveedore = await _context.PedidosProveedores
                .Include(p => p.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.IdPedidoProveedor == id);
            if (pedidosProveedore == null)
            {
                return NotFound();
            }

            return View(pedidosProveedore);
        }

        // POST: PedidosProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PedidosProveedores == null)
            {
                return Problem("Entity set 'TiendaContext.PedidosProveedores'  is null.");
            }
            var pedidosProveedore = await _context.PedidosProveedores.FindAsync(id);
            if (pedidosProveedore != null)
            {
                _context.PedidosProveedores.Remove(pedidosProveedore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidosProveedoreExists(int id)
        {
          return (_context.PedidosProveedores?.Any(e => e.IdPedidoProveedor == id)).GetValueOrDefault();
        }
    }
}
