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
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ICE_Webserver.Controllers
{
    public class PredefinedMessagesController : BaseController
    {
        #pragma warning disable CS0618
        // GET: PredefinedMessages
        public async Task<ActionResult> Index()
        {
            List<Broadcast> predefinedMessages = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/PredefinedMessagesAPI");

            if (apiResponse.IsSuccessStatusCode)
            {
                predefinedMessages = await JsonConvert.DeserializeObjectAsync<List<Broadcast>>(await apiResponse.Content.ReadAsStringAsync());
            }

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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PredefinedMessage predefinedMessage = null;

            var apiResponse = await api.Request(HttpMethod.Get, "api/BroadcastsAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                predefinedMessage = await JsonConvert.DeserializeObjectAsync<PredefinedMessage>(await apiResponse.Content.ReadAsStringAsync());
            }

            if (predefinedMessage == null)
            {
                return HttpNotFound();
            }
            return View(predefinedMessage);
        }

        // POST: PredefinedMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                var response = await api.Request(HttpMethod.Delete, "api/PredefinedMessagesAPI/", id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                await DisplayModelStateErrors(response);

            }
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
