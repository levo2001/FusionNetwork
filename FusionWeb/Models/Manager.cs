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

        [StringLength(30)]
        [Required(ErrorMessage = "  Name is required.")]
        public int Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        public string Password { get; set; }
    }
}

