using ICE_Server.DAL;
using ICE_Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICE_Server.Controllers
{
    public class SynchronizeController : ApiController
    {
        private BroadcastsRepository broadcastRepository;
        private BuildingsRepository buildingRepository;
        private DevicesRepository deviceRepository;
        private EmergencyRepository emergencyRepository;
        private LanguagesRepository languageRepository;
        private PredefinedMessagesRepository predefinedmessageRepository;

        public SynchronizeController()
        {
            this.broadcastRepository = new BroadcastsRepository(new ICEContext());
            this.buildingRepository = new BuildingsRepository(new ICEContext());
            this.deviceRepository = new DevicesRepository(new ICEContext());
            this.emergencyRepository = new EmergencyRepository(new ICEContext());
            this.languageRepository = new LanguagesRepository(new ICEContext());
            this.predefinedmessageRepository = new PredefinedMessagesRepository(new ICEContext());
        }
        // GET api/synchronize
        //[ResponseType(typeof(IEnumerable<Activity>))]
        [Route("api/synchronize/{tableType}")]
        [HttpGet]
        public IHttpActionResult Synchronize(string tableType)
        {
            if(tableType.ToLower() == "broadcasts")
            {
                return Ok(broadcastRepository.GetAll());
            }
            if(tableType.ToLower() == "buildings")
            {
                return Ok(buildingRepository.GetAll());
            }
            if(tableType.ToLower() == "devices")
            {
                return Ok(deviceRepository.GetAll());
            }
            if (tableType.ToLower() == "emergencies")
            {
                return Ok(emergencyRepository.GetAll());
            }
            if (tableType.ToLower() == "emergenciestranslated")
            {
                return Ok(emergencyRepository.GetAllTranslated());
            }
            if(tableType.ToLower() == "predefinedmessages")
            {
                return Ok(predefinedmessageRepository.GetAll());
            }
            if (tableType.ToLower() == "predefinedmessagestranslated")
            {
                return Ok(predefinedmessageRepository.GetAllTranslated());
            }
            if (tableType.ToLower() == "languages")
            {
                return Ok(languageRepository.GetAll());
            }
            else
            {
                return NotFound();
            }
            //return Mapper.DynamicMap<IEnumerable<DboActivity>, IEnumerable<Activity>>(activityRepository.SearchTodaysActivities(query));
        }
        // SYNCHRONIIIIIIIIIIZE
    }
}
