﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICE_Server.DAL;
using ICE_Server.Models;
using System.Threading.Tasks;
using System.Net.Http;
using ICE_Webserver.Models;

namespace ICE_Webserver.Controllers
{
    public class BroadcastsController : BaseController
    {
        // GET: Broadcasts
        public ActionResult Index()
        {
            var broadcasts = db.Broadcasts.Include(b => b.Emergency);
            return View(broadcasts.ToList());
        }

        // GET: Broadcasts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broadcast broadcast = db.Broadcasts.Find(id);
            if (broadcast == null)
            {
                return HttpNotFound();
            }
            return View(broadcast);
        }

        // GET: Broadcasts/Create
        public ActionResult Create()
        {
            ViewBag.EmergencyId = new SelectList(db.Emergencies, "ID", "ID");
            return View();
        }

        // POST: Broadcasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Message,Time,EmergencyId")] Broadcast broadcast)
        {
            

            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                // Request to API
                var response = await api.Request(HttpMethod.Post, "api/Broadcasts/", broadcast);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                // Otherwise check if any model state error were returned that can be displayed
                await DisplayModelStateErrors(response);

            }
            ViewBag.EmergencyId = new SelectList(db.Emergencies, "ID", "ID", broadcast.EmergencyId);
            return View(broadcast);
        }

        // GET: Broadcasts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broadcast broadcast = db.Broadcasts.Find(id);
            if (broadcast == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmergencyId = new SelectList(db.Emergencies, "ID", "ID", broadcast.EmergencyId);
            return View(broadcast);
        }

        // POST: Broadcasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Message,Time,EmergencyId")] Broadcast broadcast)
        {
            if (ModelState.IsValid)
            {
                db.Entry(broadcast).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmergencyId = new SelectList(db.Emergencies, "ID", "ID", broadcast.EmergencyId);
            return View(broadcast);
        }

        // GET: Broadcasts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broadcast broadcast = db.Broadcasts.Find(id);
            if (broadcast == null)
            {
                return HttpNotFound();
            }
            return View(broadcast);
        }

        // POST: Broadcasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Broadcast broadcast = db.Broadcasts.Find(id);
            db.Broadcasts.Remove(broadcast);
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