using ICE_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Webserver.ViewModels
{
    public class PredefinedMessageViewModel
    {
        public int ID { get; set; }

        public virtual PredefinedMessageTranslated PredefinedMessageTranslated { get; set; }

        public virtual Emergency Emergency { get; set; }

        public string EmergencyName { get; set; }

        public virtual ICollection<PredefinedMessageTranslated> PredefinedMessageTranslations { get; set; }

        public IEnumerable<Language> Languages { get; set; }

        public ICollection<Emergency> Emergencies { get; set; }

    }
}