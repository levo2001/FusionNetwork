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
        private static Order newOrder;
        private static List<DishOrder> ldo;


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
        public IActionResult Create()//FromRedirectToPayment - newOrder
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

                foreach (var id in dishIds)
                {
                    if (dict.ContainsKey(id))
                        dict[id]++;
                    else
                        dict.Add(id, 1);
                }
                int i = 0;
                int size = dishes.Count() - 1;
                Dish currentDish;


                foreach (var dish in dict)
                {
                    DishOrder d = new DishOrder();

                    d.DishId = Convert.ToInt32(dish.Key);
                    d.Quantity = dish.Value;


                    foreach (var tmp in dishes)
                    {
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


                //ViewData["quantity"] = dict;
                ViewData["total"] = total;


            }

            //newOrder = order;
            //DishOrder d = new DishOrder();
            //ldo = new List<DishOrder>();
            //for (int i = 0; i < HttpContext.Session.GetInt32("numDishes"); i++)
            //{
            //    d.DishId = Convert.ToInt32(HttpContext.Session.GetInt32("Dish" + i));
            //    d.Quantity = Convert.ToInt32(HttpContext.Session.GetInt32("DishQ" + i));
            //    if (newOrder.Dishes == null)
            //        newOrder.Dishes = new List<DishOrder>();
            //    ldo.Add(d);
            //    newOrder.Dishes.Add(d);
            //}

            globalOrder = newOrder;

            //return View(newOrder);
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
                //newOrder.Dishes = ldo;
                //order = newOrder;
                // order.id = 0;

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



                    //foreach (var dishOrd in order.Dishes)
                    //{

                    //DishOrder di = new DishOrder();

                    //di = dishOrd;
                    //dishOrd.DishId = Convert.ToInt32(HttpContext.Session.GetInt32("Dish" + i));
                    //dishOrd.OrderId = order.Id;
                    //dishOrd.Order = order;
                    //dishOrd.Dish = _context.Dish.FirstOrD efault(r => r.Id == dishOrd.DishId);
                    //dishOrd.Quantity = Convert.ToInt32(HttpContext.Session.GetInt32("DishQ" + i));
                    //if (_context.DishOrder.FirstOrDefault(r => r.DishId == dishOrd.DishId && r.OrderId == dishOrd.OrderId) == null)
                    //{
                    // _context.Add(dishOrd);
                    //_context.SaveChanges();
                    //}
                    //_context.Add(di);
                    //await _context.SaveChangesAsync();

                    //}

                    ///}
                   
                    //ViewBag.OrderDone = "הזמנתך התקבלה בהצלחה. מחכים לראות אותך";
                //foreach (var deltDish in order.Dishes)
                //{
                //    //DeleteFromCart(deltDish.Dish.Id); 
                //    var exsDish = newOrder.Dishes.FirstOrDefault(d => d.DishId == deltDish.Dish.Id);
                //    newOrder.Dishes.Remove(exsDish);
                //}


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

                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.OrderFailed = "מצטערים הזמנה נכשלה. אנא צרו קשר עם שירות לקוחות";
                }

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
