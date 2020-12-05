using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class DishOrder
    {
        public int Id { get; set; }

        [Required]
        public Dish Dish { get; set; }

        [Required]
        public int DishId { get; set; }
        
        [Required]
        public Order Order { get; set; }

        [Required]
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "הערות")]
        public string Comment { get; set; }


    }
}
