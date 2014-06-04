using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealDB.Models
{
    public class DishIngredient
    {
        [Key, ForeignKey("Dish"), ColumnAttribute(Order=0)]
        public int DishId { get; set; }
        [Key, ForeignKey("Ingredient"), ColumnAttribute(Order=1)]
        public int IngredientId { get; set; }
        public string Quantity { get; set; }
        public string Text { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
