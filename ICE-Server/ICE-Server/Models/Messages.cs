using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class Messages
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int EmergencyID { get; set; }

        public virtual EmergencyTypes EmergencyTypes { get; set; }
    }
}