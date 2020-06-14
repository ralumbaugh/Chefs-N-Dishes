using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get; set;}
        [Required (ErrorMessage="First name is required")]
        public string FirstName {get; set;}
        [Required (ErrorMessage="Last name is required")]
        public string LastName {get; set;}
        [Required (ErrorMessage="Please put in a date of birth")]
        [DataType(DataType.Date)]

        public DateTime DateOfBirth {get; set;}
        public List<Dish> CreatedDishes {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}