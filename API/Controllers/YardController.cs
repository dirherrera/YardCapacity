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
using API.Models;

namespace API.Controllers
{
    public class YardController : ApiController
    {
        private YardCapacityDBContext db = new YardCapacityDBContext();

        // GET: api/Yard
        public IQueryable<Yard> GetYard()
        {
            return db.Yard;
        }

        // GET: api/Yard/5
        [ResponseType(typeof(Yard))]
        public IHttpActionResult GetYard(int id)
        {
            Yard yard = db.Yard.SingleOrDefault(m => m.YardId == id);
            if (yard == null)
            {
                return NotFound();
            }

            return Ok(yard);
        }

        // PUT: api/Yard/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutYard(int id, Yard yard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != yard.YardId)
            {
                return BadRequest();
            }

            db.Entry(yard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YardExists(id))
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

        // POST: api/Yard
        [ResponseType(typeof(Yard))]
        public IHttpActionResult PostYard(Yard yard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Yard.Add(yard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = yard.YardId }, yard);
        }

        // DELETE: api/Yard/5
        [ResponseType(typeof(Yard))]
        public IHttpActionResult DeleteYard(int id)
        {
            Yard yard = db.Yard.Find(id);
            if (yard == null)
            {
                return NotFound();
            }

            db.Yard.Remove(yard);
            db.SaveChanges();

            return Ok(yard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool YardExists(int id)
        {
            return db.Yard.Count(e => e.YardId == id) > 0;
        }
    }
}