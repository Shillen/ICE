using ICE_Server.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace ICE_Server.Models
{
    // This class makes sure that the identity models use integers instead of strings and that the
    // ActivitiesDbContext is used.
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomUserStore : UserStore<User, Role, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ICEContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<Role, int, CustomUserRole>
    {
        public CustomRoleStore(ICEContext context)
            : base(context)
        {
        }
    }

    public class CustomClaimsPrincipal : ClaimsPrincipal
    {
        public CustomClaimsPrincipal(ClaimsPrincipal principal)
            : base(principal)
        { }

        public int UserId
        {
            get { return int.Parse(this.FindFirst(ClaimTypes.NameIdentifier).Value); }
        }

        public string Role
        {
            get { return this.FindFirst(ClaimTypes.Role).Value; }
        }

        public int RoleId
        {
            get { return int.Parse(this.FindFirst("RoleId").Value); }
        }
    }
}