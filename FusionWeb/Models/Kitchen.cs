using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Kitchen
    {
        public int Id { get; set; }

        [Display(Name = "מטבח ישראלי")]
        public int IsraelId { get; set; }

        [Display(Name = "מטבח אסיאתי")]
        public int AsianId { get; set; }

        [Display(Name = "מטבח אמריקאי")]
        public int AmericanId { get; set;}

        [Display(Name = "מטבח איטלקי")]
        public int ItalianId { get; set; }


    }
}
