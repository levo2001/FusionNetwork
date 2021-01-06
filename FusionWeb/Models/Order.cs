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

        [Required(ErrorMessage = "Client is required.")]
        public Client Client { get; set; }

      
        //public string CreditOwnerName { get; set; }
        //public string cvv{ get; set; }
        //public string expiryYear { get; set; }

        //public string expiryMonth { get; set; }
        //public int cardNumber { get; set; }
       
        //public Credit Credit { get; set; }

        [Display (Name = "סהכ לתשלום")]
        public int Total{ get; set; }

        public ICollection<DishOrder> Dishes { get; set; }

        //public Cart Cart { get; set; }


    }
}
