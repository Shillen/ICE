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
using ICE_Server.Models;

namespace ICE_Server.Controllers
{
    public class BroadcastsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Broadcasts
        public IQueryable<Broadcasts> GetBroadcasts()
        {
            return db.Broadcasts;
        }

        // GET: api/Broadcasts/5
        [ResponseType(typeof(Broadcasts))]
        public IHttpActionResult GetBroadcasts(int id)
        {
            Broadcasts broadcasts = db.Broadcasts.Find(id);
            if (broadcasts == null)
            {
                return NotFound();
            }

            return Ok(broadcasts);
        }

        // PUT: api/Broadcasts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBroadcasts(int id, Broadcasts broadcasts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != broadcasts.ID)
            {
                return BadRequest();
            }

            db.Entry(broadcasts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BroadcastsExists(id))
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

        // POST: api/Broadcasts
        [ResponseType(typeof(Broadcasts))]
        public IHttpActionResult PostBroadcasts(Broadcasts broadcasts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Broadcasts.Add(broadcasts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = broadcasts.ID }, broadcasts);
        }

        // DELETE: api/Broadcasts/5
        [ResponseType(typeof(Broadcasts))]
        public IHttpActionResult DeleteBroadcasts(int id)
        {
            Broadcasts broadcasts = db.Broadcasts.Find(id);
            if (broadcasts == null)
            {
                return NotFound();
            }

            db.Broadcasts.Remove(broadcasts);
            db.SaveChanges();

            return Ok(broadcasts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BroadcastsExists(int id)
        {
            return db.Broadcasts.Count(e => e.ID == id) > 0;
        }
    }
}