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
using Newtonsoft.Json;

namespace FusionWeb.Controllers
{
    public class DishesController : Controller
    {
        private readonly FusionWebContext _context;
        private static Dictionary<Dish,int> ldishes;

        public DishesController(FusionWebContext context)
        {
            _context = context;
        }

        // GET: Dishes
        
        public async Task<IActionResult> Index()
        {
            var dishes = from d in _context.Dish
                                  orderby d.KitchenDish ascending
                                  select d;

            return View(await dishes.ToListAsync());

        }
        public IActionResult RedirectToPayment()
        {
            
            string cart = HttpContext.Session.GetString("Cart");
            var dishes = new List<Dish>();
            ViewData["Dish"] = dishes;
            // dishes = ViewData["Dish"].
            Order newOrder = new Order();

            if (cart != null)
            {
                string[] dishIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);

                dishes = _context.Dish.Where(x => dishIds.Contains(x.Id.ToString())).ToList();
                
                //DishOrder d = new DishOrder();
                //Order newOrder = new Order();

                Dictionary<string, int> dict = new Dictionary<string, int>();

                double total = 0;
                foreach (var d in dishes)
                {
                    total += d.Price* ViewBag.quantity[d.Id.ToString()];
                }
                newOrder.Total = Convert.ToInt32(total);
                

                //ViewData["quantity"] = dict;
                ViewData["total"] = total;


            }

            return RedirectToAction("Create", "Orders", newOrder ) ;
            /*
            double total = 0;
            DishOrder d = new DishOrder();
            Order newOrder = new Order();
            HttpContext.Session.SetInt32("numDishes", ldishes.Count());
            int i = 0;
            foreach (var dish in ldishes)
            {
                d.DishId = dish.Key.Id;
                d.Quantity = dish.Value;
                d.Dish = dish.Key;
           
                HttpContext.Session.SetInt32("Dish" + i, dish.Key.Id);
                HttpContext.Session.SetInt32("DishQ" + i, dish.Value);
                total += dish.Key.Price * dish.Value;
                if (newOrder.Dishes == null)
                    newOrder.Dishes = new List<DishOrder>();
                newOrder.Dishes.Add(d);
                i++;
            }
            newOrder.Total = Convert.ToInt32(total);
            return RedirectToAction("Create", "Orders", newOrder);
               
           //return View("");
           */
        }

        // GET: DishesCart

        public async Task<IActionResult> Cart()
        {/*
            // save dictionary to session
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
             HttpContext.Session.SetString("foo", JsonConvert.SerializeObject(ldishes.ToArray(), Formatting.Indented, jsonSerializerSettings));

            // get dictionary from session
             string val = HttpContext.Session.GetString("foo");
             Dictionary<Dish, int> aa2 = JsonConvert.DeserializeObject<KeyValuePair<Dish, int>[]>(val, jsonSerializerSettings).ToDictionary(kv => kv.Key, kv => kv.Value);
           */

            string cart = HttpContext.Session.GetString("Cart");
            var dishes = new List<Dish>();
            if (cart != null)
            {
                string[] dishIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);

                dishes = _context.Dish.Where(x => dishIds.Contains(x.Id.ToString())).ToList();

                Dictionary<string, int> dict = new Dictionary<string, int>();
                foreach (var id in dishIds)
                {
                    if (dict.ContainsKey(id))
                        dict[id]++;
                    else
                        dict.Add(id, 1);
                }

                ViewData["quantity"] = dict;
                ViewData["Dish"] = dishes;
            }
            return View(dishes);

        }
        public async Task<IActionResult> DeleteFromCart(int id)
        {
            var exsDish = ldishes.Keys.FirstOrDefault(d => d.Id == id);
            ldishes.Remove(exsDish) ;

            return View("Cart",ldishes);
        }

        public async Task<IActionResult> AddToCart(int id)
        {

                string cart = HttpContext.Session.GetString("Cart");
                if (cart == null)
                    cart = "";

                cart += id + ",";
                HttpContext.Session.SetString("Cart", cart);

                return RedirectToAction("Cart");
            /*
            if (ldishes == null)
                ldishes = new Dictionary<Dish, int>();
            var dish = _context.Dish.FirstOrDefault(d => d.Id == id);//where, change!! where the id exsist in the string i save in the session!
            var exsDish = ldishes.Keys.FirstOrDefault(d => d.Id == id);
            if (exsDish != null)
                ldishes[exsDish] += 1;
            else
                ldishes.Add(dish, 1);
            return RedirectToAction("Cart", "Dishes", ldishes);
                    
             */
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
            ldishes.Remove(dish);
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
