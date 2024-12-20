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
    public class ModosPagosController : Controller
    {
        private readonly TiendaContext _context;

        public ModosPagosController(TiendaContext context)
        {
            _context = context;
        }

        // GET: ModosPagos
        public async Task<IActionResult> Index()
        {
              return _context.ModosPagos != null ? 
                          View(await _context.ModosPagos.ToListAsync()) :
                          Problem("Entity set 'TiendaContext.ModosPagos'  is null.");
        }

        // GET: ModosPagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ModosPagos == null)
            {
                return NotFound();
            }

            var modosPago = await _context.ModosPagos
                .FirstOrDefaultAsync(m => m.IdModoPago == id);
            if (modosPago == null)
            {
                return NotFound();
            }

            return View(modosPago);
        }

        // GET: ModosPagos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModosPagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdModoPago,Descripcion")] ModosPago modosPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modosPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modosPago);
        }

        // GET: ModosPagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ModosPagos == null)
            {
                return NotFound();
            }

            var modosPago = await _context.ModosPagos.FindAsync(id);
            if (modosPago == null)
            {
                return NotFound();
            }
            return View(modosPago);
        }

        // POST: ModosPagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdModoPago,Descripcion")] ModosPago modosPago)
        {
            if (id != modosPago.IdModoPago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modosPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModosPagoExists(modosPago.IdModoPago))
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
            return View(modosPago);
        }

        // GET: ModosPagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ModosPagos == null)
            {
                return NotFound();
            }

            var modosPago = await _context.ModosPagos
                .FirstOrDefaultAsync(m => m.IdModoPago == id);
            if (modosPago == null)
            {
                return NotFound();
            }

            return View(modosPago);
        }

        // POST: ModosPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ModosPagos == null)
            {
                return Problem("Entity set 'TiendaContext.ModosPagos'  is null.");
            }
            var modosPago = await _context.ModosPagos.FindAsync(id);
            if (modosPago != null)
            {
                _context.ModosPagos.Remove(modosPago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModosPagoExists(int id)
        {
          return (_context.ModosPagos?.Any(e => e.IdModoPago == id)).GetValueOrDefault();
        }
    }
}
