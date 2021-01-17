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
    public class OrdersController : Controller
    {
        private readonly FusionWebContext _context;
        /*private static Order newOrder;
        private static List<DishOrder> ldo;
        */

        private static Order globalOrder;

        public OrdersController(FusionWebContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.Include(x => x.Dishes).ThenInclude(x => x.Dish).Include(x => x.Client).ToListAsync());
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
        public IActionResult Create()//FromRedirectToPayment - newOrder
        {
            string cart = HttpContext.Session.GetString("Cart");
            var dishes = new List<Dish>();
            Order newOrder = new Order();

            if (cart != null)
            {
                string[] dishIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);
                //Get the all info of the dishes from DB
                dishes = _context.Dish.Where(x => dishIds.Contains(x.Id.ToString())).ToList();

                Dictionary<string, int> dict = new Dictionary<string, int>();

                double total = 0;
                //Create dictionary
                foreach (var id in dishIds)
                {
                    if (dict.ContainsKey(id))
                        dict[id]++;
                    else
                        dict.Add(id, 1);
                }

                int i = 0;
                Dish currentDish;

                foreach (var dish in dict)
                {
                    DishOrder d = new DishOrder();

                    d.DishId = int.Parse(dish.Key);
                    d.Quantity = dish.Value;

                    //Update total order
                    foreach (var tmp in dishes)
                    {
                        //Find the price of the dish in the dict
                        if (tmp.Id == Convert.ToInt32(dish.Key))
                        {
                            currentDish = tmp;
                            total += (currentDish.Price * dish.Value);
                            break;
                        }
                    }

                    if (newOrder.Dishes == null)
                        newOrder.Dishes = new List<DishOrder>();
                    newOrder.Dishes.Add(d);

                }
                newOrder.Total = Convert.ToInt32(total);
            }

            //For using in create post
            globalOrder = newOrder;
            
            return View(newOrder);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Total")] Order order, Client client
                                                , string cardNumber, string expiryMonth, string expiryYear,
                                                  string cvv, string CreditOwnerName)
        {
            //if (ModelState.IsValid)
                ViewData["Order"] = "order";
                int j = 0;
                order = globalOrder;

                //check if client did order in the past.
                var existsClient = _context.Client.FirstOrDefault(c => c.Id == client.Id);

                //if is new client, add him to the system.
                if (existsClient == null)
                {
                    _context.Client.Add(client);
                    _context.SaveChanges();
                }

                bool success = true;//should use payment paramters for perform payment. now ignore it.     :))))))))))))))))BE HAPPY))))))))))))))))))

                if (success)
                    {
                        //enter the order to DB
                        order.Client = client;
                        _context.Add(order);
                        await _context.SaveChangesAsync();

                    globalOrder = order;

                    string cart = HttpContext.Session.GetString("Cart");
                    string[] DishesIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    string[] Ids_NonDuplicate;
                
                    Ids_NonDuplicate = RemoveDuplicates(DishesIds);

                    foreach ( var id in Ids_NonDuplicate)
                    {
                        DeleteDIshesOrder(int.Parse(id), order);
                    }  
                        HttpContext.Session.SetString("Cart","");
                    }
               // else
                 //   {
                     //  ViewBag.OrderFailed = "מצטערים הזמנה נכשלה. אנא צרו קשר עם שירות לקוחות";
                   // }

            //}

            return View(order);
        }


        public static string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }



        public /*async Task<IActionResult>*/ void DeleteDIshesOrder(int id , Order order)
        {
 
             var exsDish = order.Dishes.FirstOrDefault(d => d.DishId == id);
             order.Dishes.Remove(exsDish);

            return;
        }


        public async Task<IActionResult> DeleteFromCart(int id)
        {
            string cart = HttpContext.Session.GetString("Cart");
            string[] DishesIds = cart.Split(",", StringSplitOptions.RemoveEmptyEntries);

            DishesIds = DishesIds.Where(w => w != id.ToString() ).ToArray();

            string AfterDelete = null;
            
            for( int i=0; i <DishesIds.Length; i++)
            {
                AfterDelete += DishesIds[i] + ",";


            }
            if(AfterDelete == null)
                HttpContext.Session.SetString("Cart", "");
            else
                HttpContext.Session.SetString("Cart", AfterDelete);

            return RedirectToAction("Cart","Dishes");        
        }

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
