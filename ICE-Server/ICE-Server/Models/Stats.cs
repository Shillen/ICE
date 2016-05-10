using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class Stats
    {
        public int BroadcastCount { get; set; }
        public int BuildingCount { get; set; }
        public int EmergenciesCount { get; set; }
        public int PredefinedMessagesCount { get; set; }
        public int DevicesCount { get; set; }
    }
}