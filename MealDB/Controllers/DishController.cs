﻿using System;
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

namespace MealDB.Controllers
{
    public class DishController : ApiController
    {
        private MealDBContext db = new MealDBContext();

        // GET api/Dish
        public IQueryable<Dish> GetDishes()
        {
            return db.Dishes;
        }

        // GET api/Dish/5
        [ResponseType(typeof(Dish))]
        public IHttpActionResult GetDish(int id)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }

        // PUT api/Dish/5
        public IHttpActionResult PutDish(int id, Dish dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dish.DishId)
            {
                return BadRequest();
            }

            db.Entry(dish).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
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

        // POST api/Dish
        [ResponseType(typeof(Dish))]
        public IHttpActionResult PostDish(Dish dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dishes.Add(dish);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dish.DishId }, dish);
        }

        // DELETE api/Dish/5
        [ResponseType(typeof(Dish))]
        public IHttpActionResult DeleteDish(int id)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            db.Dishes.Remove(dish);
            db.SaveChanges();

            return Ok(dish);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DishExists(int id)
        {
            return db.Dishes.Count(e => e.DishId == id) > 0;
        }
    }
}