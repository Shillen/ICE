using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ICE_Server.Repository;
using ICE_Server.Models;


namespace ICE_Server.Controllers
{
    public class DevicesController : ApiController
    {
        
        DevicesRepository _repository = new DevicesRepository();

        public IEnumerable<Device> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var product = _repository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
