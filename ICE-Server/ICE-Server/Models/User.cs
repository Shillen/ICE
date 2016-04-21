using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserRole UserRole { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public int IP { get; set; }
    }
    public enum UserRole
    {
        Admin, ICE, User
    }
}