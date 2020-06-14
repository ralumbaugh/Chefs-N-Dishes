using System;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get; set;}
        [Required (ErrorMessage="Name of Dish is required")]
        public string Name {get; set;}
        [Required (ErrorMessage="Calories are required")]
        [Range(1, double.PositiveInfinity, ErrorMessage="Calories must be greater than 0")]
        public int? Calories {get; set;}
        [Required (ErrorMessage="Description is required")]
        public string Description {get; set;}
        [Required (ErrorMessage="Tastiness level is required")]
        [Range(1,5,ErrorMessage="Tastiness must be between 1 and 5")]
        public int? Tastiness {get; set;}
        [Required (ErrorMessage="Please select a chef for this dish")]
        public int ChefId {get; set;}
        public Chef Creator {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}