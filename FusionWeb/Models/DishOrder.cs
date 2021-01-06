using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class DishOrder
    {
        public Dish Dish { get; set; }

        public int DishId { get; set; }
        
        public Order Order { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        //[Display(Name = "הערות")]
        //public string Comment { get; set; }


    }
}
