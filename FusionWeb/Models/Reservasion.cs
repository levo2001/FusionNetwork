using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Reservasion
    {

        public int Id { get; set; }

        public Client Client { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "תאריך")]
        [Range(0, 999.99)]
        public DateTime DateTime { get; set; }

        [Range(1, 10)]
        public int NumOfDinners { get; set; }

        [Display (Name ="הערות")]
        public string Note { get; set; }

        [Display(Name = "סוג מטבח")]
        [Required(ErrorMessage = "need to pick a kitchen ")]
        //public Kitchen KitchenId { get; set; }

        //  [Required]
        // [Range(1,4)]
        // [Display(Name ="בחירת מטבח מבין ארבעת המטבחים")]
        //public int Kitchen { get; set; }
    }
}
