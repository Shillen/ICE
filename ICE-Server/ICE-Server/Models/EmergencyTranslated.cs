using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    /// <summary>
	/// View Model of the Database object EmergencyTranslated.
	/// It contains the translation of an Emergency name.
	/// </summary>
    public class EmergencyTranslated
    {
        /// <summary>
        /// Get/set the emergency ID from <see cref="Emergency"/>
        /// </summary>
        [Key][Column(Order=0)]
        [ForeignKey("Emergency")]
        public int EmergencyID { get; set; }

        /// <summary>
        /// Get/set the language ID from <see cref="Language"/>
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Language")]
        public int LanguageID { get; set; }

        /// <summary>
        /// Emergency object
        /// </summary>
        public Emergency Emergency { get; set; }

        /// <summary>
        /// Language object
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Get/set the emergency name translated.
        /// </summary>
        public string Name { get; set; }

    }
}