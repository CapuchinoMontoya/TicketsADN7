using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;

namespace TicketsADN7.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly TicketsContext _context;

        public UsuariosController(TicketsContext context)
        {
            _context = context;
        }
        
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuario
                .Include(u => u.Departamento)
                .Include(u => u.Rol)
                .ToListAsync(); // Materializa aquí

            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.Departamento)
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoID"] = new SelectList(_context.Set<Departamento>(), "DepartamentoID", "Descripcion");
            ViewData["RolID"] = new SelectList(_context.Set<Rol>(), "RolID", "Descripcion");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioID,NombreUsuario,Contrasena,NombreCompleto,Email,RolID,DepartamentoID,Activo,Telefono")] Usuario usuario)
        {
            ModelState.Remove("Rol");
            ModelState.Remove("Departamento");
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoID"] = new SelectList(_context.Set<Departamento>(), "DepartamentoID", "Descripcion", usuario.DepartamentoID);
            ViewData["RolID"] = new SelectList(_context.Set<Rol>(), "RolID", "Descripcion", usuario.RolID);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoID"] = new SelectList(_context.Set<Departamento>(), "DepartamentoID", "Descripcion", usuario.DepartamentoID);
            ViewData["RolID"] = new SelectList(_context.Set<Rol>(), "RolID", "Descripcion", usuario.RolID);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioID,NombreUsuario,Contrasena,NombreCompleto,Email,RolID,DepartamentoID,Activo,Telefono")] Usuario usuario)
        {
            if (id != usuario.UsuarioID)
            {
                return NotFound();
            }

            ModelState.Remove("Rol");
            ModelState.Remove("Departamento");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsuarioID))
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
            ViewData["DepartamentoID"] = new SelectList(_context.Set<Departamento>(), "DepartamentoID", "Descripcion", usuario.DepartamentoID);
            ViewData["RolID"] = new SelectList(_context.Set<Rol>(), "RolID", "Descripcion", usuario.RolID);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.Departamento)
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.UsuarioID == id);
        }
    }
}
