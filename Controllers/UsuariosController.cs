using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;
using LIBRARY.COMMON.Crypto;
using Microsoft.Extensions.Options;

namespace TicketsADN7.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly TicketsContext _context;
        private readonly string _key;

        public UsuariosController(TicketsContext context, IOptions<EncryptionSettings> encryptionOptions)
        {
            _context = context;
            _key = encryptionOptions.Value.Key;
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
            ViewData["DepartamentoID"] = new SelectList(_context.Set<Departamento>(), "DepartamentoID", "NombreDepartamento");
            ViewData["RolID"] = new SelectList(_context.Set<Rol>(), "RolID", "NombreRol");
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

            if (_context.Usuario.Any(u => u.NombreUsuario == usuario.NombreUsuario))
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya está en uso.");
            }

            if (_context.Usuario.Any(u => u.Email == usuario.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
            }

            if (_context.Usuario.Any(u => u.Telefono == usuario.Telefono))
            {
                ModelState.AddModelError("Telefono", "El número de teléfono ya está registrado.");
            }

            if (ModelState.IsValid)
            {
                usuario.Contrasena = CryptionHelper.Encrypt(usuario.Contrasena, _key);
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = $"Usuario creado correctamente";

                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoID"] = new SelectList(_context.Set<Departamento>(), "DepartamentoID", "NombreDepartamento", usuario.DepartamentoID);
            ViewData["RolID"] = new SelectList(_context.Set<Rol>(), "RolID", "NombreRol", usuario.RolID);

            TempData["ToastrType"] = "error";
            TempData["ToastrMessage"] = $"Error el intentar registrar un nuevo usuario";

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
            usuario.Contrasena = CryptionHelper.Decrypt(usuario.Contrasena, _key);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoID"] = new SelectList(_context.Set<Departamento>(), "DepartamentoID", "NombreDepartamento", usuario.DepartamentoID);
            ViewData["RolID"] = new SelectList(_context.Set<Rol>(), "RolID", "NombreRol", usuario.RolID);
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

            if (_context.Usuario.Any(u => u.NombreUsuario == usuario.NombreUsuario))
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya está en uso.");
            }

            if (_context.Usuario.Any(u => u.Email == usuario.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
            }

            if (_context.Usuario.Any(u => u.Telefono == usuario.Telefono))
            {
                ModelState.AddModelError("Telefono", "El número de teléfono ya está registrado.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Contrasena = CryptionHelper.Encrypt(usuario.Contrasena, _key);
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    TempData["ToastrType"] = "success";
                    TempData["ToastrMessage"] = $"Usuario editado correctamente";
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
            ViewData["DepartamentoID"] = new SelectList(_context.Set<Departamento>(), "DepartamentoID", "NombreDepartamento", usuario.DepartamentoID);
            ViewData["RolID"] = new SelectList(_context.Set<Rol>(), "RolID", "NombreRol", usuario.RolID);

            TempData["ToastrType"] = "error";
            TempData["ToastrMessage"] = $"Error el intentar editar al usuario";

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
