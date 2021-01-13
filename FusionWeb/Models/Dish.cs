using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    [Serializable()]
    public class Dish
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "שדה חובה* ")]
        [Display(Name = "שם מנה")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "שדה חובה* ")]
        [Display(Name = "מחיר מנה")]
        [Range(0, 200)]
        public double Price { get; set; }

       
        [Display(Name = "תיאור מנה")]
        public string Description { get; set; }

    [Required(ErrorMessage = "שדה חובה* ")]
        [Display(Name = "תמונה")]
        [Url]
        public string Image { get; set; }


        public ICollection<DishOrder> Orders { get; set; }

        [Required(ErrorMessage = "שדה חובה* ")]
        public int KitchenDish { get; set; }



    }
}