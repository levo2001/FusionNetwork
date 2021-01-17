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
    public class ReservationsController : Controller
    {
        private readonly FusionWebContext _context;
         
        public ReservationsController(FusionWebContext context)
        {
            _context = context;   

        } 

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservasion.Include(x => x.Client).ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservasion.Include(x => x.Client)
                .FirstOrDefaultAsync(m => m.Id == id) ;
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,DateTime,NumOfDinners,Note,Kitchen")] Reservation reservation, Client client)
        {
            ViewData["Reservation"] = "orderres";
            var existclient = _context.Client.FirstOrDefault(c => c.Id == client.Id);
            if (existclient == null)
            {
                _context.Client.Add(client);
                _context.SaveChanges();

            }
            bool succes = true;
            if (succes)
            {
                reservation.Client = client;
                _context.Reservasion.Add(reservation);
                _context.SaveChanges();

            }
            else
            {

            }
            return View(reservation);
        }

        //// GET: Reservations/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var reservation = await _context.Reservasion.FindAsync(id);
        //        if (reservation == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(reservation);
        //    }

        //// POST: Reservations/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,NumOfDinners,Note,Kitchen")] Reservation reservation)
        //{
        //    if (id != reservation.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(reservation);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ReservationExists(reservation.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(reservation);
        //}

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservasion.Include(x => x.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservasion.FindAsync(id);
            _context.Reservasion.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservasion.Any(e => e.Id == id);
        }
    }
}
