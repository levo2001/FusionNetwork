using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "שדה חובה* ")]
        public Client Client { get; set; }

        [Display (Name = "סהכ לתשלום")]
        public int Total{ get; set; }

        public ICollection<DishOrder> Dishes { get; set; }



    }
}
