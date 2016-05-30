using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ICE_Server.Models
{
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        [Key]
        public override int Id { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "UserNameRequired")]
        [Required(ErrorMessage = "User name is required")]
        //[MaxLength(30, ErrorMessage = "User name must be less than 30 caracteres")]
        //[MinLength(3, ErrorMessage = "User name must have at least 3 caracteres")]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|_|-)+", ErrorMessage = "User name can only have letters, numbers and the special caracteres '_' and '-'")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public override string Email { get; set; }

        // Password, already exists in IdentityUser

        [ForeignKey("Role")]
        [Required(ErrorMessage = "The user must have a role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public string Ip { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(
                this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim("RoleId", RoleId.ToString()));

            if (Claims != null)
            {
                foreach (CustomUserClaim claim in Claims)
                {
                    userIdentity.AddClaim(new Claim(claim.ClaimType, claim.ClaimValue));
                }
            }

            return userIdentity;
        }
    }

}