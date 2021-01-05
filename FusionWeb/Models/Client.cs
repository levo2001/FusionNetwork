using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Client
    {
      public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "שם מלא")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "אימייל")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Address is required.")]
        [Display(Name = "כתובת")]
        public string Address { get; set; }

        [Display(Name = "עיר")]
        public string City { get; set; }
        public ICollection<Reservation> Reservasions { get; set; }
        public ICollection <Contact> Contacts{ get; set; }
        public ICollection<DishOrder> Dishes { get; set; }
       
        [Required]
        [Phone]
        [Display(Name ="מס טלפון")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
        public string PhoneNumber { get; set; }        
    
    }
}
