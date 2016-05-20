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
    public class EmergencyAPIController : BaseController
    {
        private EmergencyRepository emergencyRepository;

        public EmergencyAPIController()
        {
            this.emergencyRepository = new EmergencyRepository(new ICEContext());
        }

        // GET: api/EmergencyAPI
        [Route("api/EmergencyAPI")]
        public IEnumerable<Emergency> GetAll()
        {
            return emergencyRepository.GetAll();
        }
        // GET: api/EmergencyTranslated
        [Route("api/EmergencyTranslated")]
        public IEnumerable<EmergencyTranslated> GetAllTranslated()
        {
            return emergencyRepository.GetAllTranslated();
        }

        // GET: api/EmergencyAPI/5
        [ResponseType(typeof(Emergency))]
        public IHttpActionResult GetEmergency(int id)
        {
            return Ok(emergencyRepository.Get(id));
        }

        // GET: api/EmergencyTranslated/5
        [ResponseType(typeof(EmergencyTranslated))]
        [Route("api/EmergencyTranslated")]
        public IEnumerable<EmergencyTranslated> GetEmergencyTranslations(int id)
        {
            return emergencyRepository.GetEmergencyTranslations(id);
        }

        // POST: api/EmergencyAPI
        [ResponseType(typeof(Emergency))]
        [HttpPost]
        [Route("api/EmergencyAPI")]
        public IHttpActionResult Insert(Emergency item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            emergencyRepository.Insert(item);

            return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
        }

        // POST: api/EmergencyTranslated
        [ResponseType(typeof(EmergencyTranslated))]
        [HttpPost]
        [Route("api/EmergencyTranslated")]
        public IHttpActionResult InsertTranslations(List<EmergencyTranslated> itemList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            emergencyRepository.InsertTranslations(itemList);

            return Ok(itemList);

        }


        // PUT: api/EmergencyAPI/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/EmergencyAPI")]
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

        // DELETE: api/EmergencyAPI/5
        [ResponseType(typeof(Emergency))]
        [HttpDelete]
        [Route("api/EmergencyAPI")]
        public IHttpActionResult Delete(Emergency item)
        {
            emergencyRepository.Delete(item);

            return Ok(item);
        }

    }
}