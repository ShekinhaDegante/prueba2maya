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
    public class ClientesFrecuentesController : Controller
    {
        private readonly TiendaContext _context;

        public ClientesFrecuentesController(TiendaContext context)
        {
            _context = context;
        }

        // GET: ClientesFrecuentes
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.ClientesFrecuentes.Include(c => c.IdClienteNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: ClientesFrecuentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientesFrecuentes == null)
            {
                return NotFound();
            }

            var clientesFrecuente = await _context.ClientesFrecuentes
                .Include(c => c.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdClienteFrecuente == id);
            if (clientesFrecuente == null)
            {
                return NotFound();
            }

            return View(clientesFrecuente);
        }

        // GET: ClientesFrecuentes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: ClientesFrecuentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClienteFrecuente,IdCliente,TotalCompras,FechaUltimaCompra,PuntosAcumulados")] ClientesFrecuente clientesFrecuente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientesFrecuente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", clientesFrecuente.IdCliente);
            return View(clientesFrecuente);
        }

        // GET: ClientesFrecuentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientesFrecuentes == null)
            {
                return NotFound();
            }

            var clientesFrecuente = await _context.ClientesFrecuentes.FindAsync(id);
            if (clientesFrecuente == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", clientesFrecuente.IdCliente);
            return View(clientesFrecuente);
        }

        // POST: ClientesFrecuentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClienteFrecuente,IdCliente,TotalCompras,FechaUltimaCompra,PuntosAcumulados")] ClientesFrecuente clientesFrecuente)
        {
            if (id != clientesFrecuente.IdClienteFrecuente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientesFrecuente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesFrecuenteExists(clientesFrecuente.IdClienteFrecuente))
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
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", clientesFrecuente.IdCliente);
            return View(clientesFrecuente);
        }

        // GET: ClientesFrecuentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientesFrecuentes == null)
            {
                return NotFound();
            }

            var clientesFrecuente = await _context.ClientesFrecuentes
                .Include(c => c.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdClienteFrecuente == id);
            if (clientesFrecuente == null)
            {
                return NotFound();
            }

            return View(clientesFrecuente);
        }

        // POST: ClientesFrecuentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientesFrecuentes == null)
            {
                return Problem("Entity set 'TiendaContext.ClientesFrecuentes'  is null.");
            }
            var clientesFrecuente = await _context.ClientesFrecuentes.FindAsync(id);
            if (clientesFrecuente != null)
            {
                _context.ClientesFrecuentes.Remove(clientesFrecuente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesFrecuenteExists(int id)
        {
          return (_context.ClientesFrecuentes?.Any(e => e.IdClienteFrecuente == id)).GetValueOrDefault();
        }
    }
}
