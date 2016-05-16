using ICE_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Webserver.ViewModels
{
    public class BroadcastViewModel
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public string EmergencyName { get; set; }
        public DateTime Time { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<Emergency> Emergencies { get; set; }


    }
}