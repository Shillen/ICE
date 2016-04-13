using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    /// <summary>
	/// View Model of the Database object Building.
	/// It contains the information about a Building as the ID and name.
	/// </summary>
	public class Building
    {
        /// <summary>
        /// Get/set the building ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Get/set the building name.
        /// </summary>
        public string Name { get; set; }

        // In the future it will be good to have something like the code below.
        // It will allow the application to send specific and relevant messages to the user.
        // But because of the time, this feature will not be implemented in this phase of the project.
        // public List<Pair<float, float>> GPSPoints { get; set; }

    }
}