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
    public class BroadcastsAPIController : ApiController
    {
        private BroadcastsRepository broadcastRepository;

        public BroadcastsAPIController()
        {
            this.broadcastRepository = new BroadcastsRepository(new ICEContext());
        }
        /// <summary>
        /// Get all of the broadcasts
        /// </summary>
        // GET: api/BroadcastsAPI
        [Route("api/BroadcastsAPI")]
        public IEnumerable<Broadcast> GetAll()
        {
            return broadcastRepository.GetAll();
        }

        // GET: api/lastBroadcasts
        [Route("api/lastBroadcasts")]
        public IEnumerable<Broadcast> GetRecent()
        {
            return broadcastRepository.GetRecent();
        }

        // GET: api/BroadcastsAPI/5
        [ResponseType(typeof(Broadcast))]
        public IHttpActionResult GetBroadcast(int id)
        {
            return Ok(broadcastRepository.Get(id));
        }
        // GET: api/BroadcastBuildings/5
        [ResponseType(typeof(Building))]
        [Route("api/BroadcastBuildings")]
        public IHttpActionResult GetBroadcastBuildings(int id)
        {
            return Ok(broadcastRepository.GetBroadcastBuildings(id));
        }

        // POST: api/BroadcastsAPI
        [Route("api/BroadcastsAPI")]
        [HttpPost]
        [ResponseType(typeof(Broadcast))]
        public IHttpActionResult Insert(BroadcastItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            broadcastRepository.Insert(item);
            return (Ok());
        }

        // PUT: api/BroadcastsAPI/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Update(Broadcast item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int[] ids = { item.ID };
            broadcastRepository.Update(item, ids);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/BroadcastsAPI/5
        [ResponseType(typeof(Broadcast))]
        [HttpDelete]
        public IHttpActionResult Delete(Broadcast item)
        {
            broadcastRepository.Delete(item);

            return Ok(item);
        }

        // DELETE: api/BroadcastsAPI/5
        [ResponseType(typeof(Broadcast))]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            broadcastRepository.Delete(id);

            return Ok(id);
        }

    }
}