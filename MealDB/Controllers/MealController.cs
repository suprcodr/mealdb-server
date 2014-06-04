using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MealDB.Models;
using System.Data.Entity.Validation;

namespace MealDB.Controllers
{
    public class MealController : ApiController
    {
        private MealDBContext db = new MealDBContext();

        // GET api/Meal
        public IQueryable<Meal> GetMeals()
        {
            return db.Meals;
        }

        // GET api/Meal/2014-04-19
        [ResponseType(typeof(Meal))]
        public IHttpActionResult GetMeal(string date)
        {
            Meal meal = db.Meals.Find(date);
            if (meal == null)
            {
                return NotFound();
            }

            return Ok(meal);
        }

        private List<Dish> findOrAddDishes(List<Dish> dishes)
        {
            List<Dish> res = new List<Dish>();
            foreach(Dish dish in dishes)
            {
                Dish d = db.Dishes.Find(dish.DishId);
                if(d==null)
                {
                    d = db.Dishes.Add(dish);
                }
                res.Add(d);
            }
            return res;
        }

        // PUT api/Meal/2014-04-19
        public IHttpActionResult PutMeal(string date, Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (date != meal.MealDate)
            {
                return BadRequest();
            }

            db.Entry(meal).State = EntityState.Modified;
            meal.Dishes = findOrAddDishes(meal.Dishes);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(date))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Meal
        [ResponseType(typeof(Meal))]
        public IHttpActionResult PostMeal(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                meal.Dishes = findOrAddDishes(meal.Dishes);
                db.Meals.Add(meal);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string msg = "";
                foreach(DbEntityValidationResult eve in e.EntityValidationErrors)
                {
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage+"\n";
                    }
                }
                return BadRequest(msg);
            }

            return CreatedAtRoute("DefaultApi", new { date = meal.MealDate }, meal);
        }

        // DELETE api/Meal/2014-04-19
        [ResponseType(typeof(Meal))]
        public IHttpActionResult DeleteMeal(string date)
        {
            Meal meal = db.Meals.Find(date);
            if (meal == null)
            {
                return NotFound();
            }

            db.Meals.Remove(meal);
            db.SaveChanges();

            return Ok(meal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MealExists(string date)
        {
            return db.Meals.Count(e => e.MealDate == date) > 0;
        }
    }
}