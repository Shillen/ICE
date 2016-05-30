using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICE_Server.Models.Views.Authentication
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }

        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "The user must have a role")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Role Role { get; set; }

    }
}