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
        public void TestPushNotification()
        {
            this.pushNotification = new Pushmessage("test notification");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Broadcasts()
        {
            this.pushNotification = new Pushmessage("test notification");
            // this.pushNotification.SendAllNotifications();
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
    }
}