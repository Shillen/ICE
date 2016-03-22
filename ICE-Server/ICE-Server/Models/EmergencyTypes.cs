using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Models
{
    public class EmergencyTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmergencyTypes()
        {
            this.Broadcasts = new HashSet<Broadcasts>();
            this.Messages = new HashSet<Messages>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Broadcasts> Broadcasts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Messages> Messages { get; set; }
    }
}