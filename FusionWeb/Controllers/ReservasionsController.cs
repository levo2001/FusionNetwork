using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FusionWeb.Data;
using FusionWeb.Models;

namespace FusionWeb.Controllers
{
    public class ReservasionsController : Controller
    {
        private readonly FusionWebContext _context;

        public ReservasionsController(FusionWebContext context)
        {
            _context = context;
        }

        // GET: Reservasions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservasion.ToListAsync());
        }

        // GET: Reservasions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasion = await _context.Reservasion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservasion == null)
            {
                return NotFound();
            }

            return View(reservasion);
        }

        // GET: Reservasions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservasions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,NumOfDinners,Note")] Reservasion reservasion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservasion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservasion);
        }

        // GET: Reservasions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasion = await _context.Reservasion.FindAsync(id);
            if (reservasion == null)
            {
                return NotFound();
            }
            return View(reservasion);
        }

        // POST: Reservasions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,NumOfDinners,Note")] Reservasion reservasion)
        {
            if (id != reservasion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservasion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservasionExists(reservasion.Id))
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
            return View(reservasion);
        }

        // GET: Reservasions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasion = await _context.Reservasion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservasion == null)
            {
                return NotFound();
            }

            return View(reservasion);
        }

        // POST: Reservasions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservasion = await _context.Reservasion.FindAsync(id);
            _context.Reservasion.Remove(reservasion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservasionExists(int id)
        {
            return _context.Reservasion.Any(e => e.Id == id);
        }
    }
}
