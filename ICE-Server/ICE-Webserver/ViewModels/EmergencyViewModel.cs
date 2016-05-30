using ICE_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Webserver.ViewModels
{
    public class EmergencyViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual EmergencyTranslated EmergencyTranslated { get; set; }

        public virtual ICollection<EmergencyTranslated> EmergencyTranslations { get; set; }

        public int[] LanguageIds { get; set; }
        public string[] Translations { get; set; }

        public ICollection<Language> Languages { get; set; }

    }
}