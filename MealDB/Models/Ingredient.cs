using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MealDB.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<DishIngredient> Dishes { get; set; }
    }
}