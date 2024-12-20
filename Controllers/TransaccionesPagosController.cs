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
    public class TransaccionesPagosController : Controller
    {
        private readonly TiendaContext _context;

        public TransaccionesPagosController(TiendaContext context)
        {
            _context = context;
        }

        // GET: TransaccionesPagos
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.TransaccionesPagos.Include(t => t.IdModoPagoNavigation).Include(t => t.IdPedidoNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: TransaccionesPagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TransaccionesPagos == null)
            {
                return NotFound();
            }

            var transaccionesPago = await _context.TransaccionesPagos
                .Include(t => t.IdModoPagoNavigation)
                .Include(t => t.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);
            if (transaccionesPago == null)
            {
                return NotFound();
            }

            return View(transaccionesPago);
        }

        // GET: TransaccionesPagos/Create
        public IActionResult Create()
        {
            ViewData["IdModoPago"] = new SelectList(_context.ModosPagos, "IdModoPago", "IdModoPago");
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            return View();
        }

        // POST: TransaccionesPagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransaccion,IdPedido,IdModoPago,Monto,FechaTransaccion,Estado")] TransaccionesPago transaccionesPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccionesPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdModoPago"] = new SelectList(_context.ModosPagos, "IdModoPago", "IdModoPago", transaccionesPago.IdModoPago);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", transaccionesPago.IdPedido);
            return View(transaccionesPago);
        }

        // GET: TransaccionesPagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TransaccionesPagos == null)
            {
                return NotFound();
            }

            var transaccionesPago = await _context.TransaccionesPagos.FindAsync(id);
            if (transaccionesPago == null)
            {
                return NotFound();
            }
            ViewData["IdModoPago"] = new SelectList(_context.ModosPagos, "IdModoPago", "IdModoPago", transaccionesPago.IdModoPago);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", transaccionesPago.IdPedido);
            return View(transaccionesPago);
        }

        // POST: TransaccionesPagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransaccion,IdPedido,IdModoPago,Monto,FechaTransaccion,Estado")] TransaccionesPago transaccionesPago)
        {
            if (id != transaccionesPago.IdTransaccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccionesPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionesPagoExists(transaccionesPago.IdTransaccion))
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
            ViewData["IdModoPago"] = new SelectList(_context.ModosPagos, "IdModoPago", "IdModoPago", transaccionesPago.IdModoPago);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", transaccionesPago.IdPedido);
            return View(transaccionesPago);
        }

        // GET: TransaccionesPagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TransaccionesPagos == null)
            {
                return NotFound();
            }

            var transaccionesPago = await _context.TransaccionesPagos
                .Include(t => t.IdModoPagoNavigation)
                .Include(t => t.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);
            if (transaccionesPago == null)
            {
                return NotFound();
            }

            return View(transaccionesPago);
        }

        // POST: TransaccionesPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TransaccionesPagos == null)
            {
                return Problem("Entity set 'TiendaContext.TransaccionesPagos'  is null.");
            }
            var transaccionesPago = await _context.TransaccionesPagos.FindAsync(id);
            if (transaccionesPago != null)
            {
                _context.TransaccionesPagos.Remove(transaccionesPago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionesPagoExists(int id)
        {
          return (_context.TransaccionesPagos?.Any(e => e.IdTransaccion == id)).GetValueOrDefault();
        }
    }
}
