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
using ICE_Server.DAL;
using ICE_Server.Models;
using ICE_Server.Repository;

namespace ICE_Server.Controllers
{
    public class EmergencyAPIController : ApiController
    {
        private EmergencyRepository emergencyRepository;

        public EmergencyAPIController()
        {
            this.emergencyRepository = new EmergencyRepository(new ICEContext());
        }

        // GET: api/Emergency
        public IEnumerable<Emergency> GetAll()
        {
            return emergencyRepository.GetAll();
        }

        // GET: api/Emergency/5
        [ResponseType(typeof(Emergency))]
        public IHttpActionResult GetEmergency(int id)
        {
            return Ok(emergencyRepository.Get(id));
        }

        // POST: api/Emergency
        [ResponseType(typeof(Emergency))]
        [HttpPost]
        public IHttpActionResult Insert(Emergency item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            emergencyRepository.Insert(item);

            return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
        }

        // PUT: api/Emergency/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Update(Emergency item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int[] ids = { item.ID };
            emergencyRepository.Update(item, ids);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Emergency/5
        [ResponseType(typeof(Emergency))]
        [HttpDelete]
        public IHttpActionResult Delete(Emergency item)
        {
            emergencyRepository.Delete(item);

            return Ok(item);
        }

    }
}