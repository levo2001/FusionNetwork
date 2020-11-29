using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class DishOrder
    {


        public int Id { get; set; }

        public Dish Dish { get; set; }


        public int Quantity { get; set; }


        public string Comment { get; set; }


    }
}
