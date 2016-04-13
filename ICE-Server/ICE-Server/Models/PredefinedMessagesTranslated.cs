using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICE_Server.Models
{
    /// <summary>
	/// View Model of the Database object PredefinedMessageTranslated.
	/// It has the translated message of a predefined message.
	/// </summary>
	public class PredefinedMessageTranslated
    {
        /// <summary>
        /// Get/set the predefined message ID from PredefinedMessage/>.
        /// </summary>
        [Key]
        [Column(Order=0)]
        [ForeignKey("PredefinedMessage")]
        public int PredefinedMessageID { get; set; }

        /// <summary>
        /// Get/set the language ID from Language/>.
        /// </summary>
        [Key]
        [Column(Order=1)]
        [ForeignKey("Language")]
        public int LanguageID { get; set; }

        /// <summary>
        /// Get/set the predefined message object from PredefinedMessage/>.
        /// </summary>
        public PredefinedMessage PredefinedMessage { get; set; }

        /// <summary>
        /// Get/set the language object from Language/>.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Get/set the predefined message text translated.
        /// </summary>
        public string Message { get; set; }

    }
}