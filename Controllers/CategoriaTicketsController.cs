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
    public class CategoriaTicketsController : Controller
    {
        private readonly TicketsContext _context;

        public CategoriaTicketsController(TicketsContext context)
        {
            _context = context;
        }

        // GET: CategoriaTickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriaTicket.ToListAsync());
        }

        // GET: CategoriaTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaTicket = await _context.CategoriaTicket
                .FirstOrDefaultAsync(m => m.CategoriaID == id);
            if (categoriaTicket == null)
            {
                return NotFound();
            }

            return View(categoriaTicket);
        }

        // GET: CategoriaTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaID,Nombre,Descripcion")] CategoriaTicket categoriaTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaTicket);
        }

        // GET: CategoriaTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaTicket = await _context.CategoriaTicket.FindAsync(id);
            if (categoriaTicket == null)
            {
                return NotFound();
            }
            return View(categoriaTicket);
        }

        // POST: CategoriaTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaID,Nombre,Descripcion")] CategoriaTicket categoriaTicket)
        {
            if (id != categoriaTicket.CategoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaTicketExists(categoriaTicket.CategoriaID))
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
            return View(categoriaTicket);
        }

        // GET: CategoriaTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaTicket = await _context.CategoriaTicket
                .FirstOrDefaultAsync(m => m.CategoriaID == id);
            if (categoriaTicket == null)
            {
                return NotFound();
            }

            return View(categoriaTicket);
        }

        // POST: CategoriaTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaTicket = await _context.CategoriaTicket.FindAsync(id);
            if (categoriaTicket != null)
            {
                _context.CategoriaTicket.Remove(categoriaTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaTicketExists(int id)
        {
            return _context.CategoriaTicket.Any(e => e.CategoriaID == id);
        }
    }
}
