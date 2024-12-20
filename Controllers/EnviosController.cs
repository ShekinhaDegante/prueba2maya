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
    public class EnviosController : Controller
    {
        private readonly TiendaContext _context;

        public EnviosController(TiendaContext context)
        {
            _context = context;
        }

        // GET: Envios
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.Envios.Include(e => e.IdPedidoNavigation).Include(e => e.IdSucursalRetiroNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: Envios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Envios == null)
            {
                return NotFound();
            }

            var envio = await _context.Envios
                .Include(e => e.IdPedidoNavigation)
                .Include(e => e.IdSucursalRetiroNavigation)
                .FirstOrDefaultAsync(m => m.IdEnvio == id);
            if (envio == null)
            {
                return NotFound();
            }

            return View(envio);
        }

        // GET: Envios/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            ViewData["IdSucursalRetiro"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal");
            return View();
        }

        // POST: Envios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEnvio,IdPedido,DireccionEnvio,IdSucursalRetiro,EstadoEnvio,FechaEnvio,FechaEntrega")] Envio envio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(envio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", envio.IdPedido);
            ViewData["IdSucursalRetiro"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", envio.IdSucursalRetiro);
            return View(envio);
        }

        // GET: Envios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Envios == null)
            {
                return NotFound();
            }

            var envio = await _context.Envios.FindAsync(id);
            if (envio == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", envio.IdPedido);
            ViewData["IdSucursalRetiro"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", envio.IdSucursalRetiro);
            return View(envio);
        }

        // POST: Envios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEnvio,IdPedido,DireccionEnvio,IdSucursalRetiro,EstadoEnvio,FechaEnvio,FechaEntrega")] Envio envio)
        {
            if (id != envio.IdEnvio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(envio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvioExists(envio.IdEnvio))
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
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", envio.IdPedido);
            ViewData["IdSucursalRetiro"] = new SelectList(_context.Sucursales, "IdSucursal", "IdSucursal", envio.IdSucursalRetiro);
            return View(envio);
        }

        // GET: Envios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Envios == null)
            {
                return NotFound();
            }

            var envio = await _context.Envios
                .Include(e => e.IdPedidoNavigation)
                .Include(e => e.IdSucursalRetiroNavigation)
                .FirstOrDefaultAsync(m => m.IdEnvio == id);
            if (envio == null)
            {
                return NotFound();
            }

            return View(envio);
        }

        // POST: Envios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Envios == null)
            {
                return Problem("Entity set 'TiendaContext.Envios'  is null.");
            }
            var envio = await _context.Envios.FindAsync(id);
            if (envio != null)
            {
                _context.Envios.Remove(envio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvioExists(int id)
        {
          return (_context.Envios?.Any(e => e.IdEnvio == id)).GetValueOrDefault();
        }
    }
}
