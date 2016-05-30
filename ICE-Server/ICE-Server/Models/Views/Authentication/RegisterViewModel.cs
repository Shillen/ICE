﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICE_Server.Models.Views.Authentication
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }

        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "The user must have a role")]
        public int RoleId { get; set; }


        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }


    }
}