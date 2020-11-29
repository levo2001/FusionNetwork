using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public int Total{ get; set; }

        public ICollection<DishOrder> Dishes{ get; set; }

        

    }
}
