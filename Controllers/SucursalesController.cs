﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prueba2maya.Models;

namespace prueba2maya.Controllers
{
    public class SucursalesController : Controller
    {
        private readonly TiendaContext _context;

        public SucursalesController(TiendaContext context)
        {
            _context = context;
        }

        // GET: Sucursales
        public async Task<IActionResult> Index()
        {
              return _context.Sucursales != null ? 
                          View(await _context.Sucursales.ToListAsync()) :
                          Problem("Entity set 'TiendaContext.Sucursales'  is null.");
        }

        // GET: Sucursales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursale = await _context.Sucursales
                .FirstOrDefaultAsync(m => m.IdSucursal == id);
            if (sucursale == null)
            {
                return NotFound();
            }

            return View(sucursale);
        }

        // GET: Sucursales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sucursales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSucursal,NombreSucursal,Direccion,Telefono")] Sucursale sucursale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sucursale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sucursale);
        }

        // GET: Sucursales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursale = await _context.Sucursales.FindAsync(id);
            if (sucursale == null)
            {
                return NotFound();
            }
            return View(sucursale);
        }

        // POST: Sucursales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSucursal,NombreSucursal,Direccion,Telefono")] Sucursale sucursale)
        {
            if (id != sucursale.IdSucursal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursaleExists(sucursale.IdSucursal))
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
            return View(sucursale);
        }

        // GET: Sucursales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursale = await _context.Sucursales
                .FirstOrDefaultAsync(m => m.IdSucursal == id);
            if (sucursale == null)
            {
                return NotFound();
            }

            return View(sucursale);
        }

        // POST: Sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sucursales == null)
            {
                return Problem("Entity set 'TiendaContext.Sucursales'  is null.");
            }
            var sucursale = await _context.Sucursales.FindAsync(id);
            if (sucursale != null)
            {
                _context.Sucursales.Remove(sucursale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SucursaleExists(int id)
        {
          return (_context.Sucursales?.Any(e => e.IdSucursal == id)).GetValueOrDefault();
        }
    }
}
