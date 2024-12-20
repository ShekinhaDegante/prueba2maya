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
    public class AuditoriaInventariosController : Controller
    {
        private readonly TiendaContext _context;

        public AuditoriaInventariosController(TiendaContext context)
        {
            _context = context;
        }

        // GET: AuditoriaInventarios
        public async Task<IActionResult> Index()
        {
            var tiendaContext = _context.AuditoriaInventarios.Include(a => a.IdProductoNavigation).Include(a => a.RealizadoPorNavigation);
            return View(await tiendaContext.ToListAsync());
        }

        // GET: AuditoriaInventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AuditoriaInventarios == null)
            {
                return NotFound();
            }

            var auditoriaInventario = await _context.AuditoriaInventarios
                .Include(a => a.IdProductoNavigation)
                .Include(a => a.RealizadoPorNavigation)
                .FirstOrDefaultAsync(m => m.IdAuditoria == id);
            if (auditoriaInventario == null)
            {
                return NotFound();
            }

            return View(auditoriaInventario);
        }

        // GET: AuditoriaInventarios/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["RealizadoPor"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: AuditoriaInventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAuditoria,IdProducto,CantidadAntes,CantidadDespues,FechaAuditoria,RealizadoPor")] AuditoriaInventario auditoriaInventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auditoriaInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", auditoriaInventario.IdProducto);
            ViewData["RealizadoPor"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", auditoriaInventario.RealizadoPor);
            return View(auditoriaInventario);
        }

        // GET: AuditoriaInventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AuditoriaInventarios == null)
            {
                return NotFound();
            }

            var auditoriaInventario = await _context.AuditoriaInventarios.FindAsync(id);
            if (auditoriaInventario == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", auditoriaInventario.IdProducto);
            ViewData["RealizadoPor"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", auditoriaInventario.RealizadoPor);
            return View(auditoriaInventario);
        }

        // POST: AuditoriaInventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAuditoria,IdProducto,CantidadAntes,CantidadDespues,FechaAuditoria,RealizadoPor")] AuditoriaInventario auditoriaInventario)
        {
            if (id != auditoriaInventario.IdAuditoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditoriaInventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditoriaInventarioExists(auditoriaInventario.IdAuditoria))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", auditoriaInventario.IdProducto);
            ViewData["RealizadoPor"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", auditoriaInventario.RealizadoPor);
            return View(auditoriaInventario);
        }

        // GET: AuditoriaInventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AuditoriaInventarios == null)
            {
                return NotFound();
            }

            var auditoriaInventario = await _context.AuditoriaInventarios
                .Include(a => a.IdProductoNavigation)
                .Include(a => a.RealizadoPorNavigation)
                .FirstOrDefaultAsync(m => m.IdAuditoria == id);
            if (auditoriaInventario == null)
            {
                return NotFound();
            }

            return View(auditoriaInventario);
        }

        // POST: AuditoriaInventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AuditoriaInventarios == null)
            {
                return Problem("Entity set 'TiendaContext.AuditoriaInventarios'  is null.");
            }
            var auditoriaInventario = await _context.AuditoriaInventarios.FindAsync(id);
            if (auditoriaInventario != null)
            {
                _context.AuditoriaInventarios.Remove(auditoriaInventario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditoriaInventarioExists(int id)
        {
          return (_context.AuditoriaInventarios?.Any(e => e.IdAuditoria == id)).GetValueOrDefault();
        }
    }
}
