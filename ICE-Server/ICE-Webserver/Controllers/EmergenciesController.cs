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
using ICE_Webserver.ViewModels;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace ICE_Webserver.Controllers
{
    public class EmergenciesController : BaseController
    {
        #pragma warning disable CS0618

        // GET: Emergencies
        public async Task<ActionResult> Index()
        {
            List<Emergency> Emergencies = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/EmergencyAPI");

            if (apiResponse.IsSuccessStatusCode)
            {
                Emergencies = await JsonConvert.DeserializeObjectAsync<List<Emergency>>(await apiResponse.Content.ReadAsStringAsync());
            }

            return View(Emergencies.ToList());
        }

        // GET: Emergencies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResponse<List<EmergencyTranslated>> emergencytranslations = await HandleObjectFromRequest<List<EmergencyTranslated>>(HttpMethod.Get, "api/EmergencyTranslated/", (int)id);
            RequestResponse<Emergency> emergency = await HandleObjectFromRequest<Emergency>(HttpMethod.Get, "api/EmergencyAPI/", (int)id);
            RequestResponse<List<Language>> languages = await HandleObjectFromRequest<List<Language>>(HttpMethod.Get, "api/languagesAPI");

            if (emergency == null)
            {
                return HttpNotFound();
            }
            
            EmergencyViewModel emergencyview = new EmergencyViewModel();
            emergencyview.ID = emergency.Item.ID;
            emergencyview.Name = emergency.Item.Name;
            emergencyview.EmergencyTranslations = emergencytranslations.Item;
            emergencyview.Languages = languages.Item;
            return View(emergencyview);
        }

        // GET: Emergencies/Create
        public async Task<ActionResult> Create()
        {
            RequestResponse<List<Language>> languages = await HandleObjectFromRequest<List<Language>>(HttpMethod.Get, "api/languagesAPI");
            EmergencyViewModel emergencyview = new EmergencyViewModel();
            emergencyview.Languages = languages.Item;
            return View(emergencyview);
        }

        // POST: Emergencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Emergency emergency)
        {
            if (ModelState.IsValid)
            {
                db.Emergency.Add(emergency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emergency);
        }

        // GET: Emergencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emergency emergency = db.Emergency.Find(id);
            if (emergency == null)
            {
                return HttpNotFound();
            }
            return View(emergency);
        }

        // POST: Emergencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] Emergency emergency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emergency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emergency);
        }

        // GET: Emergencies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Emergency emergency = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/EmergencyAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                emergency = await JsonConvert.DeserializeObjectAsync<Emergency>(await apiResponse.Content.ReadAsStringAsync());
            }

            if (emergency == null)
            {
                return HttpNotFound();
            }

            return View(emergency);
        }

        // POST: Emergencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                var response = await api.Request(HttpMethod.Delete, "api/EmergencyAPI/", id);

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
        #pragma warning restore CS0618
    }
}
