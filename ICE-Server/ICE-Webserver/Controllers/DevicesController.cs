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

namespace ICE_Webserver.Controllers
{
    public class DevicesController : BaseController
    {
        #pragma warning disable CS0618
        // GET: Devices
        public async Task<ActionResult> Index()
        {
            RequestResponse<List<Device>> devices = await HandleObjectFromRequest<List<Device>>(HttpMethod.Get, "api/DevicesAPI/");

            return View(devices.Item.ToList());
        }

        // GET: Devices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RequestResponse<Device> device = await HandleObjectFromRequest<Device>(HttpMethod.Get, "api/DevicesAPI/", (int)id);

            if (device == null)
            {
                return HttpNotFound();
            }

            return View(device.Item);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DeviceID,DeviceOS")] Device device)
        {
            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                RequestResponse<Device> response = await HandleObjectFromRequest<Device>(HttpMethod.Post, "api/DevicesAPI/", device);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResponse<Device> device = await HandleObjectFromRequest<Device>(HttpMethod.Get, "api/DevicesAPI/", (int)id);
            
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DeviceID,DeviceOS")] Device device)
        {
            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                RequestResponse<Device> response = await HandleObjectFromRequest<Device>(HttpMethod.Put, "api/DevicesAPI/", device);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResponse<Device> device = await HandleObjectFromRequest<Device>(HttpMethod.Get, "api/DevicesAPI/", (int)id);
            if (device == null)
            {
                return HttpNotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await api.Request(HttpMethod.Delete, "api/DevicesAPI/", id);
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
