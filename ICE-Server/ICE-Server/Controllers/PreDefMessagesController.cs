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
    public class PreDefMessagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PreDefMessages
        public IQueryable<PreDefMessages> GetPreDefMessages()
        {
            return db.PreDefMessages;
        }

        // GET: api/PreDefMessages/5
        [ResponseType(typeof(PreDefMessages))]
        public IHttpActionResult GetPreDefMessages(int id)
        {
            PreDefMessages preDefMessages = db.PreDefMessages.Find(id);
            if (preDefMessages == null)
            {
                return NotFound();
            }

            return Ok(preDefMessages);
        }

        // PUT: api/PreDefMessages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPreDefMessages(int id, PreDefMessages preDefMessages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != preDefMessages.ID)
            {
                return BadRequest();
            }

            db.Entry(preDefMessages).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreDefMessagesExists(id))
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

        // POST: api/PreDefMessages
        [ResponseType(typeof(PreDefMessages))]
        public IHttpActionResult PostPreDefMessages(PreDefMessages preDefMessages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PreDefMessages.Add(preDefMessages);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = preDefMessages.ID }, preDefMessages);
        }

        // DELETE: api/PreDefMessages/5
        [ResponseType(typeof(PreDefMessages))]
        public IHttpActionResult DeletePreDefMessages(int id)
        {
            PreDefMessages preDefMessages = db.PreDefMessages.Find(id);
            if (preDefMessages == null)
            {
                return NotFound();
            }

            db.PreDefMessages.Remove(preDefMessages);
            db.SaveChanges();

            return Ok(preDefMessages);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreDefMessagesExists(int id)
        {
            return db.PreDefMessages.Count(e => e.ID == id) > 0;
        }
    }
}