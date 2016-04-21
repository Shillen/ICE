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
        // GET: DevicesWeb
        public async Task<ActionResult> Index()
        {
            //var broadcasts = db.Broadcasts.Include(b => b.Emergency);

            List<Device> devices = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/DevicesAPI");

            if (apiResponse.IsSuccessStatusCode)
            {
                devices = await JsonConvert.DeserializeObjectAsync<List<Device>>(await apiResponse.Content.ReadAsStringAsync());
            }

            return View(devices.ToList());
        }

        // GET: DevicesWeb/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device devices = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/DevicesAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                devices = await JsonConvert.DeserializeObjectAsync<Device>(await apiResponse.Content.ReadAsStringAsync());
            }

            if (devices == null)
            {
                return HttpNotFound();
            }

            return View(devices);
        }

        // GET: DevicesWeb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DevicesWeb/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DeviceID,DeviceOS")] Device device)
        {

            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                // Request to API
                var response = await api.Request(HttpMethod.Post, "api/DevicesAPI/", device);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                // Otherwise check if any model state error were returned that can be displayed
                await DisplayModelStateErrors(response);

            }
            return View(device);
        }

        // GET: DevicesWeb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: DevicesWeb/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DeviceID,DeviceOS")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(device);
        }

        // GET: DevicesWeb/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = null;
            var apiResponse = await api.Request(HttpMethod.Get, "api/DevicesAPI/", (int)id);

            if (apiResponse.IsSuccessStatusCode)
            {
                device = await JsonConvert.DeserializeObjectAsync<Device>(await apiResponse.Content.ReadAsStringAsync());
            }

            if (device == null)
            {
                return HttpNotFound();
            }

            return View(device);
        }

        // POST: DevicesWeb/Delete/5
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
