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
using DocumentFormat.OpenXml.Wordprocessing;

namespace TicketsADN7.Controllers
{
    public class ChecklistsController : Controller
    {
        private readonly TicketsContext _context;

        public ChecklistsController(TicketsContext context)
        {
            _context = context;
        }

        // GET: Checklists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Checklist.ToListAsync());
        }

        // GET: Checklists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Checklist checklist, List<ChecklistCampo> campos)
        {
            for (int i = 0; i < campos.Count; i++)
            {
                ModelState.Remove($"Campos[{i}].Checklist");
                ModelState.Remove($"Campos[{i}].RequiereEvidencia");
            }

            if (ModelState.IsValid && campos.Any())
            {
                checklist.Campos = campos;
                _context.Checklist.Add(checklist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Checklists");
            }
            ModelState.AddModelError("", "Debe agregar al menos un campo.");

            TempData["ToastrType"] = "error";
            TempData["ToastrMessage"] = $"Debes agregar al menos un campo.";

            return View(checklist);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklist.FindAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }

            ViewData["ChecklistCampos"] = await _context.ChecklistCampo
                .Where(x => x.ChecklistID == id)
                .ToListAsync();

            return View(checklist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChecklistID,Nombre,Descripcion")] Checklist checklist,
            List<ChecklistCampo> checklistCampos)
        {
            if (id != checklist.ChecklistID)
            {
                return NotFound();
            }
            ModelState.Remove("Campos");
            for (int i = 0; i < checklistCampos.Count; i++)
            {
                ModelState.Remove($"checklistCampos[{i}].Checklist");
                ModelState.Remove($"checklistCampos[{i}].RequiereEvidencia");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Actualizar el checklist principal
                    _context.Update(checklist);

                    // 2. Manejar los campos del checklist
                    var existingCampos = await _context.ChecklistCampo
                        .Where(c => c.ChecklistID == id)
                        .ToListAsync();

                    // Campos a actualizar/agregar
                    foreach (var campo in checklistCampos)
                    {
                        // Si tiene ID, es un campo existente
                        if (campo.ChecklistCampoID > 0)
                        {
                            var existingCampo = existingCampos.FirstOrDefault(c => c.ChecklistCampoID == campo.ChecklistCampoID);
                            if (existingCampo != null)
                            {
                                // Actualizar propiedades
                                existingCampo.NombreCampo = campo.NombreCampo;
                                existingCampo.Tipo = campo.Tipo;
                                existingCampo.RequiereEvidencia = campo.RequiereEvidencia;
                                _context.Update(existingCampo);
                            }
                        }
                        else
                        {
                            // Es un nuevo campo
                            campo.ChecklistID = checklist.ChecklistID;
                            _context.Add(campo);
                        }
                    }

                    // Campos a eliminar (los que existen en BD pero no vinieron en el formulario)
                    var camposParaEliminar = existingCampos
                        .Where(ec => !checklistCampos.Any(cc => cc.ChecklistCampoID == ec.ChecklistCampoID))
                        .ToList();

                    foreach (var campo in camposParaEliminar)
                    {
                        _context.ChecklistCampo.Remove(campo);
                    }

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Checklist actualizado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistExists(checklist.ChecklistID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Si hay errores, recargar los campos para mostrar la vista nuevamente
            ViewData["ChecklistCampos"] = checklistCampos ?? new List<ChecklistCampo>();
            return View(checklist);
        }

        private bool ChecklistExists(int id)
        {
            return _context.Checklist.Any(e => e.ChecklistID == id);
        }

    }
}
