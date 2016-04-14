using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICE_Server.DAL;
using ICE_Server.Models;

namespace ICE_Webserver.Controllers
{
    public class PredefinedMessagesController : Controller
    {
        private ICEContext db = new ICEContext();

        // GET: PredefinedMessages
        public ActionResult Index()
        {
            var predefinedMessages = db.PredefinedMessages.Include(p => p.Emergency);
            return View(predefinedMessages.ToList());
        }

        // GET: PredefinedMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PredefinedMessage predefinedMessage = db.PredefinedMessages.Find(id);
            if (predefinedMessage == null)
            {
                return HttpNotFound();
            }
            return View(predefinedMessage);
        }

        // GET: PredefinedMessages/Create
        public ActionResult Create()
        {
            ViewBag.EmergencyID = new SelectList(db.Emergencies, "ID", "ID");
            return View();
        }

        // POST: PredefinedMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmergencyID")] PredefinedMessage predefinedMessage)
        {
            if (ModelState.IsValid)
            {
                db.PredefinedMessages.Add(predefinedMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmergencyID = new SelectList(db.Emergencies, "ID", "ID", predefinedMessage.EmergencyID);
            return View(predefinedMessage);
        }

        // GET: PredefinedMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PredefinedMessage predefinedMessage = db.PredefinedMessages.Find(id);
            if (predefinedMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmergencyID = new SelectList(db.Emergencies, "ID", "ID", predefinedMessage.EmergencyID);
            return View(predefinedMessage);
        }

        // POST: PredefinedMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmergencyID")] PredefinedMessage predefinedMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(predefinedMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmergencyID = new SelectList(db.Emergencies, "ID", "ID", predefinedMessage.EmergencyID);
            return View(predefinedMessage);
        }

        // GET: PredefinedMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PredefinedMessage predefinedMessage = db.PredefinedMessages.Find(id);
            if (predefinedMessage == null)
            {
                return HttpNotFound();
            }
            return View(predefinedMessage);
        }

        // POST: PredefinedMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PredefinedMessage predefinedMessage = db.PredefinedMessages.Find(id);
            db.PredefinedMessages.Remove(predefinedMessage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
