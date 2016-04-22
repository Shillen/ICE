using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class BroadcastBuilding
    {
        /// <summary>
        /// Get/set the Broadcast ID from <see cref="Broadcast"/>
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Broadcast")]
        public int BroadcastID { get; set; }

        /// <summary>
        /// Get/set the Building ID from <see cref="Building"/>
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Building")]
        public int BuildingID { get; set; }

        /// <summary>
        /// Broadcast object
        /// </summary>
        public Broadcast Broadcast { get; set; }

        /// <summary>
        /// Building object
        /// </summary>
        public Building Building { get; set; }
    }
}