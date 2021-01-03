using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public enum Kitchen 
    {
       
        [Display(Name = "איטלקי")]
        Italian,
        [Display(Name = "ישראלי")]
        Israeli,
        [Display(Name = "אמריקאי")]
        American,
        [Display(Name = "אסייתי")]
        Asian 
    }
}
