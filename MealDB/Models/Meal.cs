using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MealDB.Models
{
    public class Meal
    {
        [Key,MaxLength(10)]
        public string MealDate { get; set; }
        public string Note { get; set; }

        public virtual List<Dish> Dishes { get; set; }
    }
}