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

        [Required(ErrorMessage = "שדה חובה* ")]
        [StringLength(30)]
        [Display(Name = "שם מלא")]
        public string Name { get; set; }

        [Required(ErrorMessage = "שדה חובה* ")]
        [EmailAddress]
        [Display(Name = "אימייל")]
        [DataType(DataType.EmailAddress, ErrorMessage = "אימייל לא תקין")]

        public string Email { get; set; }

        //[Required(ErrorMessage = "Address is required.")]
        [Display(Name = "כתובת")]
        public string Address { get; set; }

        [Display(Name = "עיר")]
        public string City { get; set; }
        public ICollection<Reservation> Reservasions { get; set; }
        public ICollection <Contact> Contacts{ get; set; }
        public ICollection<Order> Orders { get; set; }
       
        [Phone]
        [DataType(DataType.PhoneNumber, ErrorMessage = "טלפון לא תקין")]
        [Display(Name ="מס טלפון")]

        public string PhoneNumber { get; set; }        
    
    }
}
