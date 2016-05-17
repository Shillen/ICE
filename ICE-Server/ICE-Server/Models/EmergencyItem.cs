using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class EmergencyItem
    {
        /// <summary>
        /// Get/set the name of the emergency.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get/set the list of the translations.
        /// </summary>
        /// <value>The buildings.</value>
        public List<EmergencyTranslated> Translations { get; set; }

        /// <summary>
        /// Get/set the id of type of the emergency.
        /// </summary>
        public int EmergencyId { get; set; }

    }
}