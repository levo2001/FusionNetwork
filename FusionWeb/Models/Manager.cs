using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Manager
    {
        public int Id { get; set; }


        [EmailAddress]
        [Display(Name = "אימייל")]
        [StringLength(30)]
        [Required(ErrorMessage = "שדה חובה* ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "שדה חובה* ")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        public string Password { get; set; }


        public string FullName { get; set; }

    }
}

