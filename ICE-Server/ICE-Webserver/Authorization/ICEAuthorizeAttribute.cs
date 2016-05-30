using ICE_Server.Models;
using ICE_Server.Models.Views.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ICE_Webserver.Authorization
{
    public class ICEAuthorizeAttribute : AuthorizeAttribute
    {
        private List<string> roles;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Split the comma separated string into a list with roles
            roles = RolesToRolesList();

            // If no user session variable is set, don't authorize
            if (httpContext.Session["User"] == null)
            {
                return false;
            }

            // If a user is banned, don't authorize
            if (((UserViewModel)httpContext.Session["User"]).Role.Name.Equals("Banned"))
            {
                return false;
            }

            // If roles are required
            if (roles.Count > 0)
            {
                bool validRole = false;

                // Check if the user contains one of the required roles
                foreach (var role in roles)
                {
                    if (role.Equals(((UserViewModel)httpContext.Session["User"]).Role.Name))
                    {
                        validRole = true;
                        break;
                    }
                }

                // If the user does not, he is not authorized
                if (!validRole)
                {
                    return false;
                }
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // If a user is logged in, redirect him to the main page
            if (filterContext.RequestContext.HttpContext.Session["User"] != null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                {
                    { "action", "Index" },
                    { "controller", "Home" }
                });
            }

            // If a user is not authenticated, redirect him to the login page
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                {
                    { "action", "Login" },
                    { "controller", "Account" }
                    
                });
            }
        }

        private List<string> RolesToRolesList()
        {
            var carac = new char[] { ',' };
            var roles = Roles.Split(carac, StringSplitOptions.RemoveEmptyEntries);
            return roles.ToList();
        }
    }
}