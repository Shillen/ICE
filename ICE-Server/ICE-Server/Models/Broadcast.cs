using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    /// <summary>
	/// View Model of the Database object Broadcast.
	/// This model has the details of a broadcast message.
	/// </summary>
	public class Broadcast
    {
        /// <summary>
        /// Get/set of the broadcast ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Get/set the message text of the broadcast message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get/set the list of the buildings related with this broadcast message.
        /// </summary>
        /// <value>The buildings.</value>
        public List<Building> Buildings { get; set; }

        /// <summary>
        /// Get/set the time of when the message was received by the server received.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Get/set the id of type of the broadcast message type.
        /// </summary>
        [ForeignKey("Emergency")]
        public int EmergencyId { get; set; }

        /// <summary>
        /// Get/set the type of the broadcast message type.
        /// </summary>
        public Emergency Emergency { get; set; }

        /// <summary>
        /// Get/set the id of type of the broadcast message type.
        /// </summary>
        [ForeignKey("PredefinedMessage")]
        public int? PredefinedMessageID { get; set;}
        /// <summary>
        /// Get/set the type of the broadcast message type.
        /// </summary>
        public PredefinedMessage PredefinedMessage { get; set; }

    }
}