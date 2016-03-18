using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICE_Server.Controllers
{
    
    public class TestController : ApiController
    {

        [Route("api/test")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "test1", "test2" };
        }

        [Route("api/test/{id}")]
        [HttpGet]
        public string Get(int id)
        {
            return "Gettest2";
        }

        [Route("api/test")]
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [Route("api/test/{id}")]
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        [Route("api/test/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
        }


    }
}
