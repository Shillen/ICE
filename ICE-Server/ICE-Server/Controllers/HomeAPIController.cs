using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICE_Server.PushNotifications;

namespace ICE_Server.Controllers
{
    public class HomeAPIController : Controller
    {
        private Pushmessage pushNotification;
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Broadcasts()
        {
            return View();
        }
        public ActionResult Buildings()
        {
            return View();
        }
        public ActionResult Emergencies()
        {
            return View();
        }
        public ActionResult Devices()
        {
            return View();
        }
        public ActionResult Languages()
        {
            return View();
        }
        public ActionResult TestPushNotification()
        {
            this.pushNotification = new Pushmessage(Pushmessage.PushTypes.Emergency, 1, "Watch out! A swampmonster is in the building!", 0);
            return View("Index");
        }
    }
}