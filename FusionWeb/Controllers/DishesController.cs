using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FusionWeb.Data;
using FusionWeb.Models;
using Microsoft.AspNetCore.Http;

namespace FusionWeb.Controllers
{
    public class DishesController : Controller
    {
        private readonly FusionWebContext _context;

        public DishesController(FusionWebContext context)
        {
            _context = context;
        }

        // GET: Dishes
        
        public async Task<IActionResult> Index()
        {

            return View(await _context.Dish.ToListAsync());
        }


        public async Task<IActionResult> Cart(int id)
        {
            if (HttpContext.Session.GetString("cart") == null)
            {
                string myString = id.ToString();
                HttpContext.Session.SetString("cart", myString);
                var dishes = from d in _context.Dish
                             where id == d.Id
                             select d;

                return View(await dishes.ToListAsync());


            }
            else
            {
                string dishId = HttpContext.Session.GetString("cart");
                dishId += ",";
                dishId += id;
                HttpContext.Session.SetString("cart", dishId);
                string[] ids = dishId.Split(',');
                int[] myInts = ids.Select(int.Parse).ToArray();

                var c = from d in _context.Dish
                        where myInts.Contains(d.Id)
                        select d;

                //TempData["listdishes"] = c;

                //return RedirectToAction("ActionName", "Home2", new { Date = date });

                return View(await c.ToListAsync());
            }

        }



        public async Task<IActionResult> Kitchen(int Id)
        {

            var dish2 = from dish in _context.Dish
                        where dish.KitchenDish == Id
                        select dish;

            return View("Index", await dish2.ToListAsync());
        }


        public async Task<IActionResult> Search()
        {

            return View(await _context.Dish.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string Input)
        {

            int number = 0;
            if(int.TryParse(Input, out number))
            {
                 var d = from dish in _context.Dish
                        where dish.Price < number
                        select dish;

                if (d.Count() == 0)
                {
                    ViewData["Error"] = "NotExist";
                    return RedirectToAction(nameof(Index));

                    ///POPUP
                }
                else
                    return View(await d.ToListAsync());
            }
            else {

                var d1 = from dish in _context.Dish
                         where dish.Name.Contains(Input)
                         orderby dish.Name descending, dish.Price descending
                         select dish;

                if (d1.Count() == 0)
                {
                    ViewData["Error"] = "NotExist";
                    return RedirectToAction(nameof(Index));

                    ///POPUP
                }
                else
                    return View(await d1.ToListAsync());
            }
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,Image,KitchenDish")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,Image")] Dish dish)
        {
            if (id != dish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.Id))
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
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dish.FindAsync(id);
            _context.Dish.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dish.Any(e => e.Id == id);
        }
    }
}
