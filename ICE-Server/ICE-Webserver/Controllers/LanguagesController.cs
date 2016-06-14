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
    public class LanguagesController : BaseController
    {
#pragma warning disable CS0618
        // GET: Languages
        public async Task<ActionResult> Index()
        {
            RequestResponse<List<Language>> languages = await HandleObjectFromRequest<List<Language>>(HttpMethod.Get, "api/LanguagesAPI/");
            return View(languages.Item.ToList());
        }

        // GET: Languages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RequestResponse<Language> language = await HandleObjectFromRequest<Language>(HttpMethod.Get, "api/LanguagesAPI/", (int)id);

            if (language == null)
            {
                return HttpNotFound();
            }

            return View(language.Item);
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Code")] Language language)
        {
            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                // Request to API
                var response = await api.Request(HttpMethod.Post, "api/LanguagesAPI/", language);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                // Otherwise check if any model state error were returned that can be displayed
                await DisplayModelStateErrors(response);

            }
            return View(language);
        }

        // GET: Languages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResponse<Language> language = await HandleObjectFromRequest<Language>(HttpMethod.Get, "api/LanguagesAPI/", (int)id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language.Item);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Code")] Language language)
        {
            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                // Request to API
                var response = await api.Request(HttpMethod.Put, "api/LanguagesAPI/", language);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                // Otherwise check if any model state error were returned that can be displayed
                await DisplayModelStateErrors(response);

            }
            return View(language);
        }

        // GET: Languages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResponse<Language> language = await HandleObjectFromRequest<Language>(HttpMethod.Get, "api/LanguagesAPI/", (int)id);
            if (language == null)
            {
                return HttpNotFound();
            }

            return View(language.Item);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                var response = await api.Request(HttpMethod.Delete, "api/LanguagesAPI/", id);

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
