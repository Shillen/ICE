using ICE_Server.PushNotifications;
using ICE_Webserver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICE_Webserver.Controllers
{
    public class HomeController : Controller
    {
        private Pushmessage pushNotification;

        public ActionResult TestPushNotification()
        {
            this.pushNotification = new Pushmessage(Pushmessage.PushTypes.Emergency, 1, "Watch out! A swampmonster is in the building!", 0);
            return View("Index");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Settings()
        {
            ViewBag.Message = "Change application settings.";

            return View();
        }

    }
}