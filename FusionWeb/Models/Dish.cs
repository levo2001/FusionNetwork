using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Dish
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Range(0, 200)]
        public double Price { get; set; }


        public string Description { get; set; }

        [Url]
        public string Image { get; set; }


        public Order Order { get; set; }


    }
}
