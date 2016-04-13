using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICE_Server.Models
{
    /// <summary>
	/// View Model of the Database object PredefinedMessage.
	/// It has the details of a predefined message (ID and from which Emergency it belongs).
	/// </summary>
	public class PredefinedMessage
    {
        /// <summary>
        /// Get/set the predefined message ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Get/set the id of type of the predefined message type.
        /// </summary>
        [ForeignKey("Emergency")]
        public int EmergencyID { get; set; }

        /// <summary>
        /// Get/set the type of the predefined message type.
        /// </summary>
        public Emergency Emergency { get; set; } 

    }
}