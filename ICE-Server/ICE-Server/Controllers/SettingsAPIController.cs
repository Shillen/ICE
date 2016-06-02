using ICE_Server.Authorization;
using ICE_Server.DAL;
using ICE_Server.Models;
using ICE_Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ICE_Server.Controllers
{
   
    public class SettingsAPIController : BaseController
    {
        private SettingsRepository settingsRepository;
        public SettingsAPIController()
        {
            this.settingsRepository = new SettingsRepository(new ICEContext());
        }

        // GET: api/SettingsAPI
        public IEnumerable<Settings> GetAll()
        {
            return settingsRepository.GetAll();
        }

        // PUT: api/DevicesAPI/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Update(List<Settings> settings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            settingsRepository.Update(settings);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
