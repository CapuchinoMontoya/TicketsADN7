using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;
using System.Linq.Expressions;

namespace TicketsADN7.Controllers
{
    public class CatalogoProveedoresController : Controller
    {
        private readonly TicketsContext _context;

        public CatalogoProveedoresController(TicketsContext context)
        {
            _context = context;
        }

        // GET: CatalogoProveedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.CatalogoProveedores.ToListAsync());
        }

        // GET: CatalogoProveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogoProveedores = await _context.CatalogoProveedores
                .FirstOrDefaultAsync(m => m.ProveedorID == id);
            if (catalogoProveedores == null)
            {
                return NotFound();
            }

            return View(catalogoProveedores);
        }

        // GET: CatalogoProveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CatalogoProveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreProveedor,TipoProveedor,RFC,Telefono,CorreoElectronico,Direccion,Ciudad,Estado,CodigoPostal")] CatalogoProveedores catalogoProveedores)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(catalogoProveedores);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                }
            }
            return View(catalogoProveedores);
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreProveedor,TipoProveedor,RFC,Telefono,CorreoElectronico,Direccion,Ciudad,Estado,CodigoPostal")] CatalogoProveedores catalogoProveedores)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Asegurar que el campo obligatorio Estatus tiene un valor
                    catalogoProveedores.Estatus = "Activo";

                    _context.Add(catalogoProveedores);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    ModelState.AddModelError("", "Error al guardar: " + errorMessage);
                }
            }
            return View(catalogoProveedores);
        }


        // GET: CatalogoProveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogoProveedores = await _context.CatalogoProveedores.FindAsync(id);
            if (catalogoProveedores == null)
            {
                return NotFound();
            }
            return View(catalogoProveedores);
        }

        // POST: CatalogoProveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProveedorID,NombreProveedor,TipoProveedor,RFC,Telefono,CorreoElectronico,Direccion,Ciudad,Estado,CodigoPostal,FechaRegistro,Estatus")] CatalogoProveedores catalogoProveedores)
        {
            if (id != catalogoProveedores.ProveedorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogoProveedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogoProveedoresExists(catalogoProveedores.ProveedorID))
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
            return View(catalogoProveedores);
        }

        // GET: CatalogoProveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogoProveedores = await _context.CatalogoProveedores
                .FirstOrDefaultAsync(m => m.ProveedorID == id);
            if (catalogoProveedores == null)
            {
                return NotFound();
            }

            return View(catalogoProveedores);
        }

        // POST: CatalogoProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogoProveedores = await _context.CatalogoProveedores.FindAsync(id);
            if (catalogoProveedores != null)
            {
                _context.CatalogoProveedores.Remove(catalogoProveedores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogoProveedoresExists(int id)
        {
            return _context.CatalogoProveedores.Any(e => e.ProveedorID == id);
        }
    }
}
