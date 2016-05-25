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

        // GET: Broadcasts
        public async Task<ActionResult> Index()
        {
            RequestResponse<List<Broadcast>> broadcasts = await HandleObjectFromRequest<List<Broadcast>>(HttpMethod.Get, "api/BroadcastsAPI/");
            return View(broadcasts.Item.ToList());
        }

        // GET: Broadcasts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            RequestResponse<Broadcast> broadcast = await HandleObjectFromRequest<Broadcast>(HttpMethod.Get, "api/BroadcastsAPI/", (int)id);

            if (broadcast.Item == null)
            {
                return HttpNotFound();
            }

            RequestResponse<List<Building>> broadcastbuildings = await HandleObjectFromRequest<List<Building>>(HttpMethod.Get, "api/BroadcastBuildings/", (int)id);
            RequestResponse<Emergency> emergency = await HandleObjectFromRequest<Emergency>(HttpMethod.Get, "api/EmergencyAPI/", (int)broadcast.Item.EmergencyId);
            
            BroadcastViewModel broadcastview = new BroadcastViewModel();
            broadcastview.ID = broadcast.Item.ID;
            broadcastview.Message = broadcast.Item.Message;
            broadcastview.Time = broadcast.Item.Time;
            broadcastview.Buildings = broadcastbuildings.Item;
            broadcastview.EmergencyName = emergency.Item.Name;
            return View(broadcastview);
        }

        // GET: Broadcasts/Create
        public async  Task<ActionResult> Create()
        {
            RequestResponse<List<Building>> buildings = await HandleObjectFromRequest<List<Building>>(HttpMethod.Get, "api/BuildingsAPI/");
            RequestResponse<List<Emergency>> emergencies = await HandleObjectFromRequest<List<Emergency>>(HttpMethod.Get, "api/EmergencyAPI/");

            BroadcastViewModel broadcastview = new BroadcastViewModel();
            broadcastview.Time = DateTime.Now;
            broadcastview.Buildings = buildings.Item;

            List<BuildingView> buildingview = new List<BuildingView>();
            if (buildings.Item != null)
            {
                foreach (var item in buildings.Item)
                {
                    buildingview.Add(new BuildingView() { ID = item.ID, Name = item.Name, Selected = false });
                }
            }
                
            broadcastview.Buildingview = buildingview;
            broadcastview.Emergencies = emergencies.Item;
            return View(broadcastview);
        }

        // POST: Broadcasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Message,Buildings,EmergencyId")] BroadcastItem broadcastitem)
        {
            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                RequestResponse<BroadcastItem> response = await HandleObjectFromRequest<BroadcastItem>(HttpMethod.Post, "api/BroadcastsAPI/", broadcastitem);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            RequestResponse<List<Emergency>> emergencies = await HandleObjectFromRequest<List<Emergency>>(HttpMethod.Get, "api/EmergencyAPI/");
            RequestResponse<Emergency> emergency = await HandleObjectFromRequest<Emergency>(HttpMethod.Get, "api/EmergencyAPI/", (int)broadcastitem.EmergencyId);

            BroadcastViewModel broadcastview = new BroadcastViewModel();
            broadcastview.Message = broadcastitem.Message;
            broadcastview.Buildings = broadcastitem.Buildings;
            List<BuildingView> buildingview = new List<BuildingView>();
            if (broadcastitem.Buildings != null)
            {
                foreach (var item in broadcastitem.Buildings)
                {
                    buildingview.Add(new BuildingView() { ID = item.ID, Name = item.Name, Selected = false });
                }
            }
            
            broadcastview.Buildingview = buildingview;
            broadcastview.Emergencies = emergencies.Item;
            broadcastview.EmergencyName = emergency.Item.Name;
            return View(broadcastview);
        }

        // GET: Broadcasts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResponse<Broadcast> broadcast = await HandleObjectFromRequest<Broadcast>(HttpMethod.Get, "api/BroadcastsAPI/", (int)id);

            if (broadcast.Item == null)
            {
                return HttpNotFound();
            }

            return View(broadcast.Item);
        }

        // POST: Broadcasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                RequestResponse<Broadcast> response = await HandleObjectFromRequest<Broadcast>(HttpMethod.Delete, "api/BroadcastsAPI/", id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
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
