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
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }


            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            return this.RedirectToAction("Login", "Account");
        }
    }
}