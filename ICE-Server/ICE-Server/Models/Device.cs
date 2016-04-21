using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ICE_Server.Models
{
    public partial class Device
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(200)]
        public string DeviceID { get; set; }
        public OS DeviceOS { get; set; }
    }
    public enum OS
    {
        Android = 1, iOS, WinPhone
    }
}