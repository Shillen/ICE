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
using System.Web.Mvc;

namespace ICE_Server.Controllers
{
    public class PreDefinedMessagesController : ApiController
    {
        private PredefinedMessagesRepository predefinedmessageRepository;
        private ICEContext db = new ICEContext();

        public PreDefinedMessagesController()
        {
            this.predefinedmessageRepository = new PredefinedMessagesRepository(new ICEContext());
        }

        // GET: api/PreDefinedMessages
        public IEnumerable<PredefinedMessage> GetAll()
        {
            return predefinedmessageRepository.GetAll();
        }

        // GET: api/PreDefinedMessages/5
        [ResponseType(typeof(PredefinedMessage))]
        public IHttpActionResult GetPredefinedMessage(int id)
        {
            return Ok(predefinedmessageRepository.Get(id));
        }

        // POST: api/PreDefinedMessages
        [ResponseType(typeof(PredefinedMessage))]
        public IHttpActionResult Insert(PredefinedMessage item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            predefinedmessageRepository.Insert(item);

            return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
        }

        // PUT: api/PreDefinedMessages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(PredefinedMessage item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int[] ids = { item.ID };
            predefinedmessageRepository.Update(item, ids);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/PreDefinedMessages/5
        [ResponseType(typeof(PredefinedMessage))]
        public IHttpActionResult Delete(PredefinedMessage item)
        {
            predefinedmessageRepository.Delete(item);

            return Ok(item);
        }

    }
}