using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MealDB.Models
{
    public class MealDBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MealDBContext() : base("name=MealDBContext")
        {
            Database.SetInitializer<MealDBContext>(new DropCreateDatabaseAlways<MealDBContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
/*            modelBuilder.Entity<Dish>()
                        .HasMany(d => d.Meals)
                        .WithMany(m => m.Dishes)
                        .Map(m => m.MapLeftKey("DishId")
                                   .MapRightKey("Date")
                                   .ToTable("DishMeals"));*/
        }

        public System.Data.Entity.DbSet<MealDB.Models.Meal> Meals { get; set; }

        public System.Data.Entity.DbSet<MealDB.Models.Dish> Dishes { get; set; }
    
    }
}
