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
    public class DepartamentosController : Controller
    {
        private readonly TicketsContext _context;

        public DepartamentosController(TicketsContext context)
        {
            _context = context;
        }

        // GET: Departamentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamento.ToListAsync());
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento
                .FirstOrDefaultAsync(m => m.DepartamentoID == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentoID,NombreDepartamento,Descripcion")] Departamento departamento)
        {
            ModelState.Remove("Usuarios");
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = $"Departamento registrado correctamente";

                return RedirectToAction(nameof(Index));
            }

            TempData["ToastrType"] = "error";
            TempData["ToastrMessage"] = $"Error el intentar registrar un nuevo departamento";

            return View(departamento);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartamentoID,NombreDepartamento,Descripcion")] Departamento departamento)
        {
            if (id != departamento.DepartamentoID)
            {
                return NotFound();
            }

            ModelState.Remove("Usuarios");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();

                    TempData["ToastrType"] = "success";
                    TempData["ToastrMessage"] = $"Departamento editado correctamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoID))
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

            TempData["ToastrType"] = "error";
            TempData["ToastrMessage"] = $"Error el intentar editar el departamento";

            return View(departamento);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento
                .FirstOrDefaultAsync(m => m.DepartamentoID == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento != null)
            {
                _context.Departamento.Remove(departamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamento.Any(e => e.DepartamentoID == id);
        }
    }
}
