using ICE_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Webserver.ViewModels
{
    public class DashboardViewModel
    {
        public int BroadcastCount { get; set; }
        public int BuildingCount { get; set; }
        public int EmergenciesCount { get; set; }
        public int PredefinedMessagesCount { get; set; }
        public int DevicesCount { get; set; }
        public string AboutName { get; set; }
        public string AboutText { get; set; }
        public virtual ICollection<Broadcast> Broadcasts { get; set; }

    }
}