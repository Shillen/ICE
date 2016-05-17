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
    public class BuildingsAPIController : ApiController
    {
        private BuildingsRepository buildingRepository;

        public BuildingsAPIController()
        {
            this.buildingRepository = new BuildingsRepository(new ICEContext());
        }
        /// <summary>
        /// Get all of the buildings
        /// </summary>
        // GET: api/BuildingsAPI
        public IEnumerable<Building> GetAll()
        {
            return buildingRepository.GetAll();
        }

        // GET: api/BuildingsAPI/5
        [ResponseType(typeof(Building))]
        public IHttpActionResult GetBuilding(int id)
        {
            return Ok(buildingRepository.Get(id));
        }

        // POST: api/BuildingsAPI
        [ResponseType(typeof(Building))]
        [HttpPost]
        public IHttpActionResult Insert(Building item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int[] ids = { item.ID };
            buildingRepository.Insert(item);

            return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
        }

        // PUT: api/BuildingsAPI/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Update(Building item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int[] ids = { item.ID };
            buildingRepository.Update(item, ids);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/BuildingsAPI/5
        [ResponseType(typeof(Building))]
        [HttpDelete]
        public IHttpActionResult Delete(Building item)
        {
            buildingRepository.Delete(item);

            return Ok(item);
        }
        // DELETE: api/BuildingsAPI/5
        [ResponseType(typeof(Broadcast))]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            buildingRepository.Delete(id);

            return Ok(id);
        }

    }
}