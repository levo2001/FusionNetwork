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
    public class OrdersController : Controller
    {
        private readonly FusionWebContext _context;
        private static Order newOrder;
        private static List<DishOrder> ldo;

        public OrdersController(FusionWebContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.Include(x => x.Dishes).ThenInclude(x => x.Dish).Include(x => x.Client).ToListAsync());
        }

        public async Task<IActionResult> Orders()
        {
            return View(await _context.Order.Include(x => x.Dishes).ThenInclude(x => x.Dish).ToListAsync());
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

                return View(await c.ToListAsync());
            }

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [HttpGet]
        public IActionResult Create(Order order)
        {
            newOrder = order;
            DishOrder d = new DishOrder();
            ldo = new List<DishOrder>();
            for (int i = 0; i < HttpContext.Session.GetInt32("numDishes"); i++)
            {
                d.DishId = Convert.ToInt32(HttpContext.Session.GetInt32("Dish" + i));
                d.Quantity = Convert.ToInt32(HttpContext.Session.GetInt32("DishQ" + i));
                if (newOrder.Dishes == null)
                    newOrder.Dishes = new List<DishOrder>();
                ldo.Add(d);
                newOrder.Dishes.Add(d);
            }
            
            //var c = from d in _context.Dish
            //        where DishId.Contains(d.Id)
            //        select d;
            //ViewData["orderDishes"] = c;

            //ViewData["DishId"] = new SelectList(_context.Dish, "Id", "Name");
            return View(newOrder);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Total")] Order order, Client client  
                                                ,string cardNumber,string expiryMonth, string expiryYear, 
                                                  string cvv, string CreditOwnerName)
        {
            newOrder.Dishes = ldo;
            
            order = newOrder;
               // order.id = 0;
                //check if client did order in the past.
                var existsClient = _context.Client.FirstOrDefault(c => c.Id == client.Id);
                //if is new client, add him to the system.
                if (existsClient == null)
                {
                    _context.Client.Add(client);
                    _context.SaveChanges();
                }
                bool success = true;//should use payment paramters for perform payment. now ignore it.
                if (success)
                {

                    //enter the order to DB
                    order.Client = client;
                    _context.Order.Add(order);
                    _context.SaveChanges();
                //DishOrder dishOrd = new DishOrder();
                foreach (var dishOrd in order.Dishes)
                {
                    //dishOrd.DishId = Convert.ToInt32(HttpContext.Session.GetInt32("Dish" + i));
                    dishOrd.OrderId = order.Id;
                    dishOrd.Order = order;
                    dishOrd.Dish = _context.Dish.FirstOrDefault(r => r.Id == dishOrd.DishId);
                    //dishOrd.Quantity = Convert.ToInt32(HttpContext.Session.GetInt32("DishQ" + i));
                    if(_context.DishOrder.FirstOrDefault(r => r.DishId == dishOrd.DishId && r.OrderId == dishOrd.OrderId) == null)
                    {
                        _context.DishOrder.Add(dishOrd);
                        _context.SaveChanges();
                    }
                }
                //for (int i = 0; i < order.Dishes.Count(); i++)
                //{
                // dishOrd.DishId = Convert.ToInt32(HttpContext.Session.GetInt32("Dish" + i));
                //dishOrd.OrderId = order.Id;
                //dishOrd.Order = order;
                //dishOrd.Dish = _context.Dish.FirstOrDefault(r => r.Id == dishOrd.DishId);
                //dishOrd.Quantity = Convert.ToInt32(HttpContext.Session.GetInt32("DishQ" + i));
                //_context.DishOrder.Add(dishOrd);
                //    _context.SaveChanges();

                //}
                ViewBag.OrderDone = "הזמנתך התקבלה בהצלחה. מחכים לראות אותך";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.OrderFailed = "מצטערים הזמנה נכשלה. אנא צרו קשר עם שירות לקוחות";
                }
                return View(order);
        }

            //if (ModelState.IsValid)
            //{

            //    order.Dishes = new List<DishOrder>();
            //    foreach(var id in DishId)
            //    {
            //    order.Dishes.Add(new DishOrder() { DishId = id, OrderId = order.Id });

            //}

            //    _context.Add(order);

            //DishOrder item = new DishOrder();
            //item.Order = order;

            //var c = from d in _context.Dish
            //        where IdDishes.Contains((char)d.Id)
            //        select d;

            //item.Order.Cart.Dishes = (ICollection<Dish>)c;

            //await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(order);
            //}

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Total")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
