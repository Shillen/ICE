using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ICE_Server.Models
{
    /// <summary>
	/// View Model of the Database object Language.
	/// It has the details of a language object (ID and code).
	/// </summary>
	public class Language
    {
        /// <summary>
        /// Get/set the language ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Get/set the code from a language.
        /// </summary>
        public string Code { get; set; }

    }
}