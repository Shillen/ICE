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
    public class DevicesAPIController : ApiController
    {
        private DevicesRepository deviceRepository;

        public DevicesAPIController()
        {
            this.deviceRepository = new DevicesRepository(new ICEContext());
        }

        // GET: api/DevicesAPI
        public IEnumerable<Device> GetAll()
        {
            return deviceRepository.GetAll();
        }

        // GET: api/DevicesAPI/5
        [ResponseType(typeof(Device))]
        public IHttpActionResult GetDevice(int id)
        {
            return Ok(deviceRepository.Get(id));
        }

        // POST: api/DevicesAPI
        [ResponseType(typeof(Device))]
        [HttpPost]
        public IHttpActionResult Insert(Device item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            deviceRepository.Insert(item);

            return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
        }

        // PUT: api/DevicesAPI/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Update(Device item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int[] ids = { item.ID };
            deviceRepository.Update(item, ids);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/DevicesAPI/5
        [ResponseType(typeof(Device))]
        [HttpDelete]
        public IHttpActionResult Delete(Device item)
        {
            deviceRepository.Delete(item);

            return Ok(item);
        }
        // DELETE: api/DevicesAPI/5
        [ResponseType(typeof(Broadcast))]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            deviceRepository.Delete(id);

            return Ok(id);
        }

    }
}