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
using ICE_Webserver.Models;
using Newtonsoft.Json;
using ICE_Webserver.ViewModels;

namespace ICE_Webserver.Controllers
{
    public class BroadcastsController : BaseController
    {
        #pragma warning disable CS0618
        // not working yet
        public async Task<Broadcast> getBroadcast(int? id)
        {
            Broadcast broadcast = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BroadcastsAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                broadcast = await JsonConvert.DeserializeObjectAsync<Broadcast>(await apiResponse.Content.ReadAsStringAsync());
            }

            return broadcast;
        }

        // GET: Broadcasts
        public async Task<ActionResult> Index()
        {
            List<Broadcast> broadcasts = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BroadcastsAPI");

            if (apiResponse.IsSuccessStatusCode)
            {
                broadcasts = await JsonConvert.DeserializeObjectAsync<List<Broadcast>>(await apiResponse.Content.ReadAsStringAsync());
            }

            return View(broadcasts.ToList());
        }

        // GET: Broadcasts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Broadcast broadcast = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BroadcastsAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                broadcast = await JsonConvert.DeserializeObjectAsync<Broadcast>(await apiResponse.Content.ReadAsStringAsync());
            }

            if (broadcast == null)
            {
                return HttpNotFound();
            }

            List<Building> broadcastbuildings = null;
            var apiResponse2 = await api.Request(HttpMethod.Get, "api/BroadcastBuildings?id=" + id);

            if (apiResponse2.IsSuccessStatusCode)
            {
                broadcastbuildings = await JsonConvert.DeserializeObjectAsync<List<Building>>(await apiResponse2.Content.ReadAsStringAsync());
            }

            Emergency emergency = null;
            var apiResponse3 = await api.Request(HttpMethod.Get, "api/EmergencyAPI/", (int)broadcast.EmergencyId);

            if (apiResponse3.IsSuccessStatusCode)
            {
                emergency = await JsonConvert.DeserializeObjectAsync<Emergency>(await apiResponse3.Content.ReadAsStringAsync());
            }

            BroadcastViewModel broadcastview = new BroadcastViewModel();
            broadcastview.ID = broadcast.ID;
            broadcastview.Message = broadcast.Message;
            broadcastview.Time = broadcast.Time;
            broadcastview.Buildings = broadcastbuildings;
            broadcastview.EmergencyName = emergency.Name;
            return View(broadcastview);
        }

        // GET: Broadcasts/Create
        public async  Task<ActionResult> Create()
        {
            List<Building> buildings = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BuildingsAPI");

            if (apiResponse.IsSuccessStatusCode)
            {
                buildings = await JsonConvert.DeserializeObjectAsync<List<Building>>(await apiResponse.Content.ReadAsStringAsync());
            }
            List<Emergency> emergencies = null;
            var apiResponse2 = await api.Request(HttpMethod.Get, "api/EmergencyAPI");

            if (apiResponse2.IsSuccessStatusCode)
            {
                emergencies = await JsonConvert.DeserializeObjectAsync<List<Emergency>>(await apiResponse2.Content.ReadAsStringAsync());
            }

            BroadcastViewModel broadcastview = new BroadcastViewModel();
            broadcastview.Time = DateTime.Now;
            broadcastview.Buildings = buildings;

            List<BuildingView> buildingview = new List<BuildingView>();
            foreach (var item in buildings)
            {
                buildingview.Add(new BuildingView() { ID = item.ID, Name = item.Name, Selected = false });
            }
            broadcastview.Buildingview = buildingview;
            broadcastview.Emergencies = emergencies;
            return View(broadcastview);
        }

        // POST: Broadcasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Message,Buildings,EmergencyId")] BroadcastItem broadcastitem)
        {
            //broadcastitem.Buildings.Add();
            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                // Request to API
                var response = await api.Request(HttpMethod.Post, "api/BroadcastsAPI/", broadcastitem);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                

                // Otherwise check if any model state error were returned that can be displayed
                await DisplayModelStateErrors(response);

            }

            List<Emergency> emergencies = null;
            var apiResponse2 = await api.Request(HttpMethod.Get, "api/EmergencyAPI");

            if (apiResponse2.IsSuccessStatusCode)
            {
                emergencies = await JsonConvert.DeserializeObjectAsync<List<Emergency>>(await apiResponse2.Content.ReadAsStringAsync());
            }

            Emergency emergency = null;
            var apiResponse3 = await api.Request(HttpMethod.Get, "api/EmergencyAPI/", (int)broadcastitem.EmergencyId);

            if (apiResponse3.IsSuccessStatusCode)
            {
                emergency = await JsonConvert.DeserializeObjectAsync<Emergency>(await apiResponse3.Content.ReadAsStringAsync());
            }

            BroadcastViewModel broadcastview = new BroadcastViewModel();
            broadcastview.Message = broadcastitem.Message;
            broadcastview.Buildings = broadcastitem.Buildings;
            List<BuildingView> buildingview = new List<BuildingView>();
            foreach (var item in broadcastitem.Buildings)
            {
                buildingview.Add(new BuildingView() { ID = item.ID, Name = item.Name, Selected = false });
            }
            broadcastview.Buildingview = buildingview;
            broadcastview.Emergencies = emergencies;
            broadcastview.EmergencyName = emergency.Name;
            return View(broadcastview);
        }

        // GET: Broadcasts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Broadcast broadcast = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/BroadcastsAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                broadcast = await JsonConvert.DeserializeObjectAsync<Broadcast>(await apiResponse.Content.ReadAsStringAsync());
            }

            if (broadcast == null)
            {
                return HttpNotFound();
            }

            return View(broadcast);
        }

        // POST: Broadcasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                var response = await api.Request(HttpMethod.Delete, "api/BroadcastsAPI/", id);

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
