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
            return View("Index");
        }
    }
}