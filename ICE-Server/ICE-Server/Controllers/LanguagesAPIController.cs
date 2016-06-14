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
using ICE_Server.Authorization;

namespace ICE_Server.Controllers
{
    public class LanguagesAPIController : BaseController
    {
        private LanguagesRepository languageRepository;

        public LanguagesAPIController()
        {
            this.languageRepository = new LanguagesRepository(new ICEContext());
        }
        [AllowAnonymous]
        // GET: api/LanguagesAPI
        public IEnumerable<Language> GetAll()
        {
            return languageRepository.GetAll();
        }
        [AllowAnonymous]
        // GET: api/LanguagesAPI/5
        [ResponseType(typeof(Language))]
        public IHttpActionResult GetLanguage(int id)
        {
            return Ok(languageRepository.Get(id));
        }

        [ICEApiAuthorize(Roles = "Admin")]
        // POST: api/LanguagesAPI
        [ResponseType(typeof(Language))]
        [HttpPost]
        public IHttpActionResult Insert(Language item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            languageRepository.Insert(item);

            return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
        }
        [ICEApiAuthorize(Roles = "Admin")]
        // PUT: api/LanguagesAPI/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Update(Language item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int[] ids = { item.ID };
            languageRepository.Update(item, ids);

            return StatusCode(HttpStatusCode.NoContent);
        }
        [ICEApiAuthorize(Roles = "Admin")]
        // DELETE: api/LanguagesAPI/5
        [ResponseType(typeof(Language))]
        [HttpDelete]
        public IHttpActionResult Delete(Language item)
        {
            languageRepository.Delete(item);

            return Ok(item);
        }
        [ICEApiAuthorize(Roles = "Admin")]
        // DELETE: api/LanguagesAPI/5
        [ResponseType(typeof(Broadcast))]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            languageRepository.Delete(id);

            return Ok(id);
        }

    }
}