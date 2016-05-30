using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ICE_Webserver.Models;
using System.Net.Http;
using Newtonsoft.Json;
using ICE_Server.Models;

namespace ICE_Webserver.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            // Try to login the user
            var successfulLogin = await ExecuteLogin(model);

            if (successfulLogin)
            {
                // Get the redirect url from the url
                var redirectUrl = Request.QueryString["redirect_url"];

                // If a redirect url is set, redirect the user to that location
                if (redirectUrl != null && !redirectUrl.Equals(string.Empty))
                {
                    return Redirect(redirectUrl);
                }

                // Otherwise redirect to the activity index by default
                return RedirectToAction("Index", "Home");
            }

            // If we come here, the login was not successful
            // ModelState.AddModelError("Password", Resources.Global.InvalidPassword);

            return View(model);
        }

        /// <summary>
        /// Executes a login by requesting an access token, sets it in the api and creates a user
        /// session variable.
        /// </summary>
        /// <param name="model">A login model contain the user's user name and password</param>
        /// <returns>True if successfully logged in, otherwise false</returns>
        [AllowAnonymous]
        private async Task<bool> ExecuteLogin(LoginViewModel model)
        {
            var response = await api.Request(HttpMethod.Post, "api/user/login", model);

            if (response.IsSuccessStatusCode)
            {
                // If the response is successfull, it should contain an oauth access token
                var oAuthModel = JsonConvert.DeserializeObject<OAuthResponse>(await response.Content.ReadAsStringAsync());

                // Store the token in a session
                Session["AccessToken"] = oAuthModel.AccessToken;

                // Store the retrieved user in a session
                Session["User"] = oAuthModel.User;

                return true;
            }

            // Otherwise check if any model state error were returned that can be displayed
            await DisplayModelStateErrors(response);

            return false;
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session.Abandon();
            return this.RedirectToAction("Login", "Account");
        }
    }
}