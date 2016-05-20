using ICE_Server.Models;
using ICE_Webserver.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICE_Webserver.Controllers
{
    [ICEAuthorize(Roles="Admin")]
    public class SettingsController : BaseController
    {
        // GET: Settings
        public ActionResult Index()
        {
            List<Settings> model = new List<Settings>();
            model = db.Settings.ToList();
            return View(model);
        }
        // POST: Settings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<Settings> list)
        {
            if (ModelState.IsValid)
            {
                
                foreach (var i in list)
                {
                    var c = db.Settings.Where(a => a.ID.Equals(i.ID)).FirstOrDefault();
                    if (c != null)
                    {
                        c.Value = i.Value;
                    }
                    //db.Entry(i).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                ViewBag.Message = "Successfully Updated.";
                return View(list);
            }
            else
            {
                ViewBag.Message = "Failed ! Please try again.";
                return View(list);
            }
        }
    }
}