using ICE_Server.DAL;
using ICE_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICE_Server.Controllers
{

    public class StatsController : ApiController
    {
        private ICEContext context = new ICEContext();
        public StatsController()
        {

        }
        [Route("api/stats")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            Stats stats = new Stats();
            stats.BroadcastCount = context.Broadcasts.Count();
            stats.BuildingCount = context.Buildings.Count();
            stats.EmergenciesCount = context.Emergencies.Count();
            stats.PredefinedMessagesCount = context.PredefinedMessages.Count();
            stats.DevicesCount = context.Devices.Count();
            return Ok(stats);
        }
       
    }
}
