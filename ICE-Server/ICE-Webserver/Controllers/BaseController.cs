using ICE_Server.DAL;
using ICE_Server.Models;
using ICE_Webserver.Authorization;
using ICE_Webserver.Models;
using Newtonsoft.Json;
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
    public class BaseController : Controller
    {
        protected API api;
        protected ICEContext db;

        public BaseController()
        {
            api = new API();
            db = new ICEContext();
        }

        protected async Task DisplayModelStateErrors(HttpResponseMessage apiResponse)
        {

            // Otherwise check if any model state error were returned that can be displayed
            #pragma warning disable CS0618 // Type or member is obsolete
            var model = await JsonConvert.DeserializeObjectAsync<ApiResponseModel>(await apiResponse.Content.ReadAsStringAsync());
            #pragma warning restore CS0618 // Type or member is obsolete

            if (model != null && model.ModelState != null)
            {
                foreach (var error in model.ModelState)
                {
                    if (!string.IsNullOrEmpty(error.Key))
                    {
                        // An error contains both a key and a value, so it
                        // can be added to the mvc model state
                        foreach (var errorMessage in error.Value)
                        {
                            ModelState.AddModelError(error.Key, errorMessage.ToString());
                        }
                    }
                }
            }

        }
    }
}