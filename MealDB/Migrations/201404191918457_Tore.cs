namespace MealDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        DishId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Text = c.String(),
                        Source = c.String(),
                        ReferenceUrl = c.String(),
                        Dish_DishId = c.Int(),
                    })
                .PrimaryKey(t => t.DishId)
                .ForeignKey("dbo.Dishes", t => t.Dish_DishId)
                .Index(t => t.Dish_DishId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.DishIngredients",
                c => new
                    {
                        DishId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Quantity = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => new { t.DishId, t.IngredientId })
                .ForeignKey("dbo.Dishes", t => t.DishId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.DishId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientId);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        MealDate = c.String(nullable: false, maxLength: 10),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.MealDate);
            
            CreateTable(
                "dbo.CategoryDishes",
                c => new
                    {
                        Category_CategoryId = c.Int(nullable: false),
                        Dish_DishId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Dish_DishId })
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Dishes", t => t.Dish_DishId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Dish_DishId);
            
            CreateTable(
                "dbo.MealDishes",
                c => new
                    {
                        Meal_MealDate = c.String(nullable: false, maxLength: 10),
                        Dish_DishId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_MealDate, t.Dish_DishId })
                .ForeignKey("dbo.Meals", t => t.Meal_MealDate, cascadeDelete: true)
                .ForeignKey("dbo.Dishes", t => t.Dish_DishId, cascadeDelete: true)
                .Index(t => t.Meal_MealDate)
                .Index(t => t.Dish_DishId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MealDishes", "Dish_DishId", "dbo.Dishes");
            DropForeignKey("dbo.MealDishes", "Meal_MealDate", "dbo.Meals");
            DropForeignKey("dbo.DishIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.DishIngredients", "DishId", "dbo.Dishes");
            DropForeignKey("dbo.Dishes", "Dish_DishId", "dbo.Dishes");
            DropForeignKey("dbo.CategoryDishes", "Dish_DishId", "dbo.Dishes");
            DropForeignKey("dbo.CategoryDishes", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.MealDishes", new[] { "Dish_DishId" });
            DropIndex("dbo.MealDishes", new[] { "Meal_MealDate" });
            DropIndex("dbo.DishIngredients", new[] { "IngredientId" });
            DropIndex("dbo.DishIngredients", new[] { "DishId" });
            DropIndex("dbo.Dishes", new[] { "Dish_DishId" });
            DropIndex("dbo.CategoryDishes", new[] { "Dish_DishId" });
            DropIndex("dbo.CategoryDishes", new[] { "Category_CategoryId" });
            DropTable("dbo.MealDishes");
            DropTable("dbo.CategoryDishes");
            DropTable("dbo.Meals");
            DropTable("dbo.Ingredients");
            DropTable("dbo.DishIngredients");
            DropTable("dbo.Categories");
            DropTable("dbo.Dishes");
        }
    }
}
