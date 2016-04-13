﻿using ICE_Server.PushNotifications;
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
            this.pushNotification = new Pushmessage("Swampmonster", "Watch out! A swampmonster is in the building!");
            return View("Index");
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
    }
}