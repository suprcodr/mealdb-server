using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MealDB.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public string Source { get; set; }
        public string ReferenceUrl { get; set; }

        [JsonIgnore]
        public virtual List<Meal> Meals { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<Dish> GoesWith { get; set; }
        public virtual List<DishIngredient> Ingredients { get; set; }
    }
}
