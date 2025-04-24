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
    public class EstadoTicketsController : Controller
    {
        private readonly TicketsContext _context;

        public EstadoTicketsController(TicketsContext context)
        {
            _context = context;
        }

        // GET: EstadoTickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoTicket.ToListAsync());
        }

        // GET: EstadoTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoTicket = await _context.EstadoTicket
                .FirstOrDefaultAsync(m => m.EstadoID == id);
            if (estadoTicket == null)
            {
                return NotFound();
            }

            return View(estadoTicket);
        }

        // GET: EstadoTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoID,Nombre,Descripcion,EsCerrado")] EstadoTicket estadoTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoTicket);
        }

        // GET: EstadoTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoTicket = await _context.EstadoTicket.FindAsync(id);
            if (estadoTicket == null)
            {
                return NotFound();
            }
            return View(estadoTicket);
        }

        // POST: EstadoTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoID,Nombre,Descripcion,EsCerrado")] EstadoTicket estadoTicket)
        {
            if (id != estadoTicket.EstadoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoTicketExists(estadoTicket.EstadoID))
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
            return View(estadoTicket);
        }

        // GET: EstadoTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoTicket = await _context.EstadoTicket
                .FirstOrDefaultAsync(m => m.EstadoID == id);
            if (estadoTicket == null)
            {
                return NotFound();
            }

            return View(estadoTicket);
        }

        // POST: EstadoTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoTicket = await _context.EstadoTicket.FindAsync(id);
            if (estadoTicket != null)
            {
                _context.EstadoTicket.Remove(estadoTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoTicketExists(int id)
        {
            return _context.EstadoTicket.Any(e => e.EstadoID == id);
        }
    }
}
