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
    public class PredefinedMessagesController : BaseController
    {
        private PredefinedMessagesRepository predefinedmessageRepository;

        public PredefinedMessagesController()
        {
            this.predefinedmessageRepository = new PredefinedMessagesRepository(new ICEContext());
        }

        // GET: api/PredefinedMessagesAPI
        [Route("api/PredefinedMessagesAPI")]
        public IEnumerable<PredefinedMessage> GetAll()
        {
            return predefinedmessageRepository.GetAll();
        }

        // GET: api/PredefinedMessageTranslated
        [Route("api/PredefinedMessageTranslated")]
        public IEnumerable<PredefinedMessageTranslated> GetAllTranslated()
        {
            return predefinedmessageRepository.GetAllTranslated();
        }

        // GET: api/PredefinedMessagesAPI/5
        [ResponseType(typeof(PredefinedMessage))]
        [Route("api/PredefinedMessagesAPI")]
        public IHttpActionResult GetPredefinedMessage(int id)
        {
            return Ok(predefinedmessageRepository.Get(id));
        }

        // GET: api/PredefinedMessageTranslated/5
        [ResponseType(typeof(PredefinedMessageTranslated))]
        [Route("api/PredefinedMessageTranslated")]
        public IEnumerable<PredefinedMessageTranslated> GetPredefinedMessageTranslations(int id)
        {
            return predefinedmessageRepository.GetPredefinedMessageTranslations(id);
        }

        // POST: api/PredefinedMessagesAPI
        [ResponseType(typeof(PredefinedMessage))]
        [HttpPost]
        [Route("api/PredefinedMessagesAPI")]
        public IHttpActionResult Insert(PredefinedMessage item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            predefinedmessageRepository.Insert(item);

            return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
        }

        // POST: api/PredefinedMessageTranslated
        [ResponseType(typeof(PredefinedMessageTranslated))]
        [HttpPost]
        [Route("api/PredefinedMessageTranslated")]
        public IHttpActionResult InsertTranslations(List<PredefinedMessageTranslated> itemList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            predefinedmessageRepository.InsertTranslations(itemList);

            return Ok(itemList);

        }

        // PUT: api/PredefinedMessagesAPI/5
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

        // DELETE: api/PredefinedMessagesAPI/5
        [ResponseType(typeof(PredefinedMessage))]
        public IHttpActionResult Delete(PredefinedMessage item)
        {
            predefinedmessageRepository.Delete(item);

            return Ok(item);
        }

    }
}