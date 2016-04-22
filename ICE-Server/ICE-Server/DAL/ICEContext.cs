using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ICE_Server.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ICE_Server.DAL
{
    public class ICEContext : DbContext
    {

        public ICEContext() : base("ICEContext")
        {
        }

        public virtual DbSet<Broadcast> Broadcasts { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<BroadcastBuilding> BroadcastBuilding { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Emergency> Emergencies { get; set; }
        public virtual DbSet<EmergencyTranslated> EmergenciesTranslated { get; set; }
        public virtual DbSet<PredefinedMessage> PredefinedMessages { get; set; }
        public virtual DbSet<PredefinedMessageTranslated> PredefinedMessagesTranslated { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}