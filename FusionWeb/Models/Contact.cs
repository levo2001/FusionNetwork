using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Display (Name ="פרטי לקוח")]
        public Client InfoClient { get; set; }

        [Display(Name = "תוכן הפניה")]
        public string Content { get; set; }

    }
}
