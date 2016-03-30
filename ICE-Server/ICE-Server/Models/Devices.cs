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
        public string UniqueID { get; set; }
        public string DeviceOS { get; set; }
    }
}