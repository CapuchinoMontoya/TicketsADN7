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
            ModelState.Remove("Campos[0].Checklist");
            ModelState.Remove("Campos[1].Checklist");
            ModelState.Remove("Campos[2].Checklist");
            ModelState.Remove("Campos[1].RequiereEvidencia");
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
    }
}
