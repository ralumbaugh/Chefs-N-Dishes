using System;
using System.Collections.Generic;

namespace ChefsNDishes.Models
{
    public class WrapperModel
    {
        public Chef OneChef {get; set;}
        public Dish OneDish {get; set;}
        public List<Chef> AllChefs {get; set;}
        public List<Dish> AllDishes {get; set;}
    }
}