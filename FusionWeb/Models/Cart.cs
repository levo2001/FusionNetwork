using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Cart
    {
        public int Id { get; set; }

        //public ICollection<Dish> SelectedDishes { get; set; }
        public ICollection<Dish> Dishes { get; set; }

    }
}
