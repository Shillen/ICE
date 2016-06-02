using ICE_Server.Models;
using ICE_Webserver.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ICE_Webserver.Controllers
{
    [ICEAuthorize]
    public class SettingsController : BaseController
    {
        // GET: Settings
        public async Task<ActionResult> Index()
        {
            RequestResponse<List<Settings>> settings = await HandleObjectFromRequest<List<Settings>>(HttpMethod.Get, "api/SettingsAPI/");
            return View(settings.Item.ToList());
        }
        // POST: Settings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(List<Settings> list)
        {
            if (ModelState.IsValid)
            {
                // Request to API
                var response = await api.Request(HttpMethod.Put, "api/SettingsAPI/", list);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                // Otherwise check if any model state error were returned that can be displayed
                await DisplayModelStateErrors(response);

                ViewBag.Message = "Successfully Updated.";
                return View(list);
            }
            else
            {
                ViewBag.Message = "Failed ! Please try again.";
                return View(list);
            }
        }
    }
}