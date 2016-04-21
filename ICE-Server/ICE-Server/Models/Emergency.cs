using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ICE_Server.Models
{
    /// <summary>
	/// View Model of the Database object Emergency.
	/// It contains the information about an Emergency as the ID and also name.
	/// </summary>
	public class Emergency
    {

        /// <summary>
        /// Get/set the emergency ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

    }
}