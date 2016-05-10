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
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace ICE_Webserver.Controllers
{
    public class BuildingsController : BaseController
    {
        #pragma warning disable CS0618
        // GET: Buildings
        public async Task<ActionResult> Index()
        {
            List<Building> buildings = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BuildingsAPI");

            if (apiResponse.IsSuccessStatusCode)
            {
                buildings = await JsonConvert.DeserializeObjectAsync<List<Building>>(await apiResponse.Content.ReadAsStringAsync());
            }

            return View(buildings.ToList());
        }

        // GET: Buildings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Building building = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BuildingsAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                building = await JsonConvert.DeserializeObjectAsync<Building>(await apiResponse.Content.ReadAsStringAsync());
            }

            if (building == null)
            {
                return HttpNotFound();
            }

            return View(building);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name, Location")] Building building)
        {
            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                // Request to API
                var response = await api.Request(HttpMethod.Post, "api/BuildingsAPI/", building);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                // Otherwise check if any model state error were returned that can be displayed
                await DisplayModelStateErrors(response);

            }
            return View(building);
        }

        // GET: Buildings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Building building = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BuildingAPIs", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                building = await JsonConvert.DeserializeObjectAsync<Building>(await apiResponse.Content.ReadAsStringAsync());
            }
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Building building)
        {
            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                // Request to API
                var response = await api.Request(HttpMethod.Put, "api/BuildingsAPI/", building);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                // Otherwise check if any model state error were returned that can be displayed
                await DisplayModelStateErrors(response);

            }
            return View(building);
        }

        // GET: Buildings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Building building = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BuildingsAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                building = await JsonConvert.DeserializeObjectAsync<Building>(await apiResponse.Content.ReadAsStringAsync());
            }

            if (building == null)
            {
                return HttpNotFound();
            }

            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                var response = await api.Request(HttpMethod.Delete, "api/BuildingsAPI/", id);

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
