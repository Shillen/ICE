using ICE_Server.Controllers;
using ICE_Server.Models;
using ICE_Server.PushNotifications;
using ICE_Webserver.Authorization;
using ICE_Webserver.Models;
using ICE_Webserver.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ICE_Webserver.Controllers
{
    
    public class HomeController : BaseController
    {
        private Pushmessage pushNotification;

        public ActionResult TestPushNotification()
        {
            this.pushNotification = new Pushmessage(Pushmessage.PushTypes.Emergency, 1, "Watch out! A swampmonster is in the building!", 0);
            return View("Index");
        }
        #pragma warning disable CS0618
        public async Task<ActionResult> Index()
        {
            Stats stats = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/stats");

            if (apiResponse.IsSuccessStatusCode)
            {
                stats = await JsonConvert.DeserializeObjectAsync<Stats>(await apiResponse.Content.ReadAsStringAsync());
            }
            List<Broadcast> broadcasts = null;
            var apiResponse2 = await api.Request(HttpMethod.Get, "api/lastBroadcasts");

            if (apiResponse2.IsSuccessStatusCode)
            {
                broadcasts = await JsonConvert.DeserializeObjectAsync<List<Broadcast>>(await apiResponse2.Content.ReadAsStringAsync());
            }

            DashboardViewModel homeview = new DashboardViewModel();
            if (stats != null)
            {
                homeview.BroadcastCount = stats.BroadcastCount;
                homeview.BuildingCount = stats.BuildingCount;
                homeview.EmergenciesCount = stats.EmergenciesCount;
                homeview.PredefinedMessagesCount = stats.PredefinedMessagesCount;
                homeview.DevicesCount = stats.DevicesCount;
            }
            homeview.Broadcasts = broadcasts;
            return View(homeview);
        }

        public ActionResult Settings()
        {
            ViewBag.Message = "Change application settings.";

            return View();
        }

    }
}