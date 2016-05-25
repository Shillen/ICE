using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class PredefinedMessageItem
    {
        /// <summary>
        /// Get/set the name of the message.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get/set the list of the translations.
        /// </summary>
        /// <value>The buildings.</value>
        public List<PredefinedMessageTranslated> Translations { get; set; }

        /// <summary>
        /// Get/set the id of type of the message.
        /// </summary>
        public int EmergencyId { get; set; }

    }
}