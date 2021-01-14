using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Reservation
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "שדה חובה* ")]
        public Client Client { get; set; }

        [Required(ErrorMessage = "שדה חובה* ")]
        [DataType(DataType.Date)]
        [Display(Name = "תאריך")]
        //[Range(0, 999.99)]
        public DateTime DateTime { get; set; }
        [Required(ErrorMessage = "שדה חובה* ")]
        [Range(1, 10)]
        [Display(Name = "מס' סועדים")]

        public int NumOfDinners { get; set; }

        [Display (Name ="הערות")]
        public string Note { get; set; }

        [Display(Name = "סוג מטבח")]
        [Required(ErrorMessage = "שדה חובה* ")]
        public Kitchen Kitchen { get; set; }

        //  [Required]
        // [Range(1,4)]
        // [Display(Name ="בחירת מטבח מבין ארבעת המטבחים")]
        //public int Kitchen { get; set; }

        //Kitchen = DaysInWeek.Sunday;
        //if (kitchen == DaysInWeek.Monday)

    }
}
