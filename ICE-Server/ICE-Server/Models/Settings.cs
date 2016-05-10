using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public partial class Settings
    {
        [Key]
        public int ID { get; set; }
        public string Option { get; set; }
        public string Value { get; set; }
    }
}