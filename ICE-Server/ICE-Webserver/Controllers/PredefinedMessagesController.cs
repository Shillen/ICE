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
using ICE_Webserver.ViewModels;

namespace ICE_Webserver.Controllers
{
    public class PredefinedMessagesController : BaseController
    {
        #pragma warning disable CS0618
        // GET: PredefinedMessages
        public async Task<ActionResult> Index()
        {
            RequestResponse<List<PredefinedMessage>> predefinedMessages = await HandleObjectFromRequest<List<PredefinedMessage>>(HttpMethod.Get, "api/PredefinedMessagesAPI");
            return View(predefinedMessages.Item.ToList());
        }

        // GET: PredefinedMessages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResponse<PredefinedMessage> predefMessage = await HandleObjectFromRequest<PredefinedMessage>(HttpMethod.Get, "api/PredefinedMessagesAPI/", (int)id);
            RequestResponse<List<PredefinedMessageTranslated>> messagetranslations = await HandleObjectFromRequest<List<PredefinedMessageTranslated>>(HttpMethod.Get, "api/PredefinedMessageTranslated/", (int)id);
            RequestResponse<Emergency> emergency = await HandleObjectFromRequest<Emergency>(HttpMethod.Get, "api/EmergencyAPI/", (int)id);
            RequestResponse<List<Language>> languages = await HandleObjectFromRequest<List<Language>>(HttpMethod.Get, "api/languagesAPI");
            if (emergency == null)
            {
                return HttpNotFound();
            }

            PredefinedMessageViewModel messageview = new PredefinedMessageViewModel();
            messageview.ID = id ?? default(int);
            messageview.EmergencyName = emergency.Item.Name;
            messageview.PredefinedMessageTranslations = messagetranslations.Item;
            messageview.Languages = languages.Item;
            messageview.Name = predefMessage.Item.Name;
            return View(messageview);
        }

        // GET: PredefinedMessages/Create
        public async Task<ActionResult> Create()
        {
            // Get all the data from the database via API calls
            RequestResponse<List<Language>> languages = await HandleObjectFromRequest<List<Language>>(HttpMethod.Get, "api/languagesAPI");
            RequestResponse<List<Emergency>> emergencies = await HandleObjectFromRequest<List<Emergency>>(HttpMethod.Get, "api/EmergencyAPI/");

            // Create the new viewmodel and add the data from the API calls
            PredefinedMessageViewModel messageview = new PredefinedMessageViewModel();
            messageview.Languages = languages.Item;
            messageview.Emergencies = emergencies.Item;

            // Return the new viewmodel to the webpage
            return View(messageview);
        }

        // POST: PredefinedMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PredefinedMessageViewModel viewresult)
        {
            PredefinedMessageItem messageItem = new PredefinedMessageItem();
            messageItem.EmergencyId = Int32.Parse(viewresult.EmergencyId);
            messageItem.Name = viewresult.Name;
            if (viewresult.Translations != null)
            {
                messageItem.Translations = new List<PredefinedMessageTranslated>();
                var i = 0;
                foreach (string value in viewresult.Translations)
                {
                    messageItem.Translations.Add(new PredefinedMessageTranslated() { LanguageID = viewresult.LanguageIds[i], Message = value });
                    i++;
                }
            }

            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                RequestResponse<PredefinedMessageItem> response = await HandleObjectFromRequest<PredefinedMessageItem>(HttpMethod.Post, "api/PredefinedMessagesAPI/", messageItem);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            RequestResponse<List<Language>> languages = await HandleObjectFromRequest<List<Language>>(HttpMethod.Get, "api/languagesAPI");
            PredefinedMessageViewModel messageview = new PredefinedMessageViewModel();
            messageview.Languages = languages.Item;
            return View(messageview);
        }

        // GET: PredefinedMessages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResponse<PredefinedMessage> predefMessage = await HandleObjectFromRequest<PredefinedMessage>(HttpMethod.Get, "api/PredefinedMessagesAPI/", (int)id);
            if (predefMessage == null)
            {
                return HttpNotFound();
            }
            RequestResponse<List<PredefinedMessageTranslated>> predefMessageTranslations = await HandleObjectFromRequest<List<PredefinedMessageTranslated>>(HttpMethod.Get, "api/PredefinedMessageTranslated/", (int)id);
            RequestResponse<List<Language>> languages = await HandleObjectFromRequest<List<Language>>(HttpMethod.Get, "api/languagesAPI");
            RequestResponse<List<Emergency>> emergencies = await HandleObjectFromRequest<List<Emergency>>(HttpMethod.Get, "api/EmergencyAPI/");


            PredefinedMessageViewModel messageview = new PredefinedMessageViewModel();
            messageview.ID = predefMessage.Item.ID;
            messageview.Name = predefMessage.Item.Name;
            messageview.PredefinedMessageTranslations = predefMessageTranslations.Item;
            messageview.Languages = languages.Item;
            messageview.Emergencies = emergencies.Item;
            return View(messageview);
        }

        // POST: PredefinedMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PredefinedMessageViewModel viewresult)
        {
            PredefinedMessageItem predefMessageItem = new PredefinedMessageItem();
            predefMessageItem.ID = viewresult.ID;
            predefMessageItem.Name = viewresult.Name;
            if (viewresult.Translations != null)
            {
                predefMessageItem.Translations = new List<PredefinedMessageTranslated>();
                var i = 0;
                foreach (string value in viewresult.Translations)
                {
                    predefMessageItem.Translations.Add(new PredefinedMessageTranslated() { PredefinedMessageID = predefMessageItem.ID, LanguageID = viewresult.LanguageIds[i], Message = value });
                    i++;
                }
            }

            // Only if the model is valid it will be send to API
            if (ModelState.IsValid)
            {
                RequestResponse<PredefinedMessageItem> response = await HandleObjectFromRequest<PredefinedMessageItem>(HttpMethod.Put, "api/PredefinedMessageAPI/", predefMessageItem);

                // Check API's response
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(viewresult);
        }

        // GET: PredefinedMessages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RequestResponse<PredefinedMessage> predefinedMessage = await HandleObjectFromRequest<PredefinedMessage>(HttpMethod.Get, "api/PredefinedMessagesAPI/", (int)id);

            if (predefinedMessage == null)
            {
                return HttpNotFound();
            }
            return View(predefinedMessage.Item);
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
