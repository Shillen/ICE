using ICE_Server.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ICE_Server.Authorization
{
    public class ICEApiAuthorizeAttribute : AuthorizeAttribute
    {
        private CustomClaimsPrincipal currentUser;
        private List<string> roles;

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // Fetch the current user
            currentUser = new CustomClaimsPrincipal((ClaimsPrincipal) HttpContext.Current.User);

            // Split the comma separated string into a list with roles
            roles = RolesToRolesList();

            // If the user is not authenticated, he's not authorized
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
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
                    if (role.Equals(currentUser.Role))
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

        private List<string> RolesToRolesList()
        {
            return new List<string> (Roles.Split(new char[','], StringSplitOptions.RemoveEmptyEntries));
        }
    }
}