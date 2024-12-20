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
    public class HistorialPedidosController : Controller
    {
        private readonly TiendaContext _context;

        public HistorialPedidosController(TiendaContext context)
        {
            _context = context;
        }

        // GET: HistorialPedidos
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.HistorialPedidos.Include(h => h.ActualizadoPorNavigation).Include(h => h.IdPedidoNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: HistorialPedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HistorialPedidos == null)
            {
                return NotFound();
            }

            var historialPedido = await _context.HistorialPedidos
                .Include(h => h.ActualizadoPorNavigation)
                .Include(h => h.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdHistorialPedido == id);
            if (historialPedido == null)
            {
                return NotFound();
            }

            return View(historialPedido);
        }

        // GET: HistorialPedidos/Create
        public IActionResult Create()
        {
            ViewData["ActualizadoPor"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            return View();
        }

        // POST: HistorialPedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHistorialPedido,IdPedido,EstadoAnterior,EstadoNuevo,FechaCambio,ActualizadoPor")] HistorialPedido historialPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActualizadoPor"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", historialPedido.ActualizadoPor);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", historialPedido.IdPedido);
            return View(historialPedido);
        }

        // GET: HistorialPedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HistorialPedidos == null)
            {
                return NotFound();
            }

            var historialPedido = await _context.HistorialPedidos.FindAsync(id);
            if (historialPedido == null)
            {
                return NotFound();
            }
            ViewData["ActualizadoPor"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", historialPedido.ActualizadoPor);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", historialPedido.IdPedido);
            return View(historialPedido);
        }

        // POST: HistorialPedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHistorialPedido,IdPedido,EstadoAnterior,EstadoNuevo,FechaCambio,ActualizadoPor")] HistorialPedido historialPedido)
        {
            if (id != historialPedido.IdHistorialPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialPedidoExists(historialPedido.IdHistorialPedido))
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
            ViewData["ActualizadoPor"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", historialPedido.ActualizadoPor);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", historialPedido.IdPedido);
            return View(historialPedido);
        }

        // GET: HistorialPedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HistorialPedidos == null)
            {
                return NotFound();
            }

            var historialPedido = await _context.HistorialPedidos
                .Include(h => h.ActualizadoPorNavigation)
                .Include(h => h.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdHistorialPedido == id);
            if (historialPedido == null)
            {
                return NotFound();
            }

            return View(historialPedido);
        }

        // POST: HistorialPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HistorialPedidos == null)
            {
                return Problem("Entity set 'TiendaContext.HistorialPedidos'  is null.");
            }
            var historialPedido = await _context.HistorialPedidos.FindAsync(id);
            if (historialPedido != null)
            {
                _context.HistorialPedidos.Remove(historialPedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialPedidoExists(int id)
        {
          return (_context.HistorialPedidos?.Any(e => e.IdHistorialPedido == id)).GetValueOrDefault();
        }
    }
}
