using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class Broadcasts
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public string Buildings { get; set; }
        public System.DateTime Time { get; set; }
        public int EmergencyID { get; set; }

        public virtual EmergencyTypes EmergencyTypes { get; set; }
    }
}